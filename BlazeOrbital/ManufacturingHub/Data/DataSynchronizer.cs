using BlazeOrbital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace BlazeOrbital.ManufacturingHub.Data;

// This service synchronizes the Sqlite DB with both the backend server and the browser's IndexedDb storage
class DataSynchronizer
{
    public const string SqliteDbFilename = "app.db";
    private readonly Task firstTimeSetupTask;
    private readonly IDbContextFactory<ClientSideDbContext> dbContextFactory;
    private readonly ManufacturingData.ManufacturingDataClient manufacturingData;
    private bool isSynchronizing;

    public DataSynchronizer(IJSRuntime js, IDbContextFactory<ClientSideDbContext> dbContextFactory, ManufacturingData.ManufacturingDataClient manufacturingData)
    {
        this.dbContextFactory = dbContextFactory;
        this.manufacturingData = manufacturingData;
        firstTimeSetupTask = FirstTimeSetupAsync(js);
    }

    public int SyncCompleted { get; private set; }
    public int SyncTotal { get; private set; }

    public async Task<ClientSideDbContext> GetPreparedDbContextAsync()
    {
        await firstTimeSetupTask;
        return await dbContextFactory.CreateDbContextAsync();
    }

    public void SynchronizeInBackground()
    {
        _ = EnsureSynchronizingAsync();
    }

    public event Action? OnUpdate;
    public event Action<Exception>? OnError;

    private async Task FirstTimeSetupAsync(IJSRuntime js)
    {
        var module = await js.InvokeAsync<IJSObjectReference>("import", "./dbstorage.js");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
        {
            await module.InvokeVoidAsync("synchronizeFileWithIndexedDb", SqliteDbFilename);
        }

        using var db = await dbContextFactory.CreateDbContextAsync();
        await db.Database.EnsureCreatedAsync();
    }

    private async Task EnsureSynchronizingAsync()
    {
        // Don't run multiple syncs in parallel. This simple logic is adequate because of single-threadedness.
        if (isSynchronizing)
        {
            return;
        }

        try
        {
            isSynchronizing = true;
            SyncCompleted = 0;
            SyncTotal = 0;

            // Get a DB context
            using var db = await GetPreparedDbContextAsync();
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // Begin fetching any updates to the dataset from the backend server
            var mostRecentUpdate = db.Parts.OrderByDescending(p => p.ModifiedTicks).FirstOrDefault()?.ModifiedTicks;

            var connection = db.Database.GetDbConnection();
            connection.Open();

            while (true)
            {
                var request = new PartsRequest { MaxCount = 5000, ModifiedSinceTicks = mostRecentUpdate ?? -1 };
                var response = await manufacturingData.GetPartsAsync(request);
                var syncRemaining = response.ModifiedCount - response.Parts.Count;
                SyncCompleted += response.Parts.Count;
                SyncTotal = SyncCompleted + syncRemaining;

                if (response.Parts.Count == 0)
                {
                    break;
                }
                else
                {
                    mostRecentUpdate = response.Parts.Last().ModifiedTicks;
                    BulkInsert(connection, response.Parts);
                    OnUpdate?.Invoke();
                }
            }
        }
        catch (Exception ex)
        {
            // TODO: use logger also
            OnError?.Invoke(ex);
        }
        finally
        {
            isSynchronizing = false;
        }
    }

    private void BulkInsert(DbConnection connection, IEnumerable<Part> parts)
    {
        // Since we're inserting so much data, we can save a huge amount of time by dropping down below EF Core and
        // using the fastest bulk insertion technique for Sqlite.
        using (var transaction = connection.BeginTransaction())
        {
            var command = connection.CreateCommand();
            var partId = AddNamedParameter(command, "$PartId");
            var category = AddNamedParameter(command, "$Category");
            var subcategory = AddNamedParameter(command, "$Subcategory");
            var name = AddNamedParameter(command, "$Name");
            var location = AddNamedParameter(command, "$Location");
            var stock = AddNamedParameter(command, "$Stock");
            var priceCents = AddNamedParameter(command, "$PriceCents");
            var modifiedTicks = AddNamedParameter(command, "$ModifiedTicks");

            command.CommandText =
                $"INSERT OR REPLACE INTO Parts (PartId, Category, Subcategory, Name, Location, Stock, PriceCents, ModifiedTicks) " +
                $"VALUES ({partId.ParameterName}, {category.ParameterName}, {subcategory.ParameterName}, {name.ParameterName}, {location.ParameterName}, {stock.ParameterName}, {priceCents.ParameterName}, {modifiedTicks.ParameterName})";

            foreach (var part in parts)
            {
                partId.Value = part.PartId;
                category.Value = part.Category;
                subcategory.Value = part.Subcategory;
                name.Value = part.Name;
                location.Value = part.Location;
                stock.Value = part.Stock;
                priceCents.Value = part.PriceCents;
                modifiedTicks.Value = part.ModifiedTicks;
                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        static DbParameter AddNamedParameter(DbCommand command, string name)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}
