using BlazeOrbital.CentralServer.Data;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazeOrbital.Data;

[Authorize]
public class ManufacturingDataService : ManufacturingData.ManufacturingDataBase
{
    private readonly ApplicationDbContext db;

    public ManufacturingDataService(ApplicationDbContext db)
    {
        this.db = db;
    }

    public override Task<DashboardReply> GetDashboardData(DashboardRequest request, ServerCallContext context)
    {
        return Task.FromResult(new DashboardReply
        {
            ProjectsBookedValue = 38_000_000,
            NextDeliveryDueInMs = (long)TimeSpan.FromHours(53).TotalMilliseconds,
            StaffOnSite = 441,
            FactoryUptimeMs = (long)TimeSpan.FromDays(152).TotalMilliseconds,
            ServicingTasksDue = 7,
            MachinesStopped = 3,
        });
    }

    public override async Task<PartsReply> GetParts(PartsRequest request, ServerCallContext context)
    {
        var modifiedParts = db.Parts
            .OrderBy(p => p.ModifiedTicks)
            .Where(p => p.ModifiedTicks > request.ModifiedSinceTicks);

        var reply = new PartsReply();
        reply.ModifiedCount = await modifiedParts.CountAsync();
        reply.Parts.AddRange(await modifiedParts.Take(request.MaxCount).ToListAsync());
        return reply;
    }
}
