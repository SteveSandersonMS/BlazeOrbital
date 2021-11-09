using BlazeOrbital.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazeOrbital.ManufacturingHub.Data;

internal class ClientSideDbContext : DbContext
{
    public DbSet<Part> Parts { get; set; } = default!;

    public ClientSideDbContext(DbContextOptions<ClientSideDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Part>().HasIndex(nameof(Part.ModifiedTicks), nameof(Part.PartId));
        modelBuilder.Entity<Part>().HasIndex(nameof(Part.Category), nameof(Part.Subcategory));
        modelBuilder.Entity<Part>().HasIndex(x => x.Stock);
        modelBuilder.Entity<Part>().HasIndex(x => x.Name);
        modelBuilder.Entity<Part>().Property(x => x.Name).UseCollation("nocase");
    }
}
