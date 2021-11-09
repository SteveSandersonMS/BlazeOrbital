using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using BlazeOrbital.Data;

namespace BlazeOrbital.CentralServer.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Part>().HasIndex(nameof(Part.ModifiedTicks), nameof(Part.PartId));
    }

    // Inventory
    public DbSet<Part> Parts { get; set; } = default!;
}
