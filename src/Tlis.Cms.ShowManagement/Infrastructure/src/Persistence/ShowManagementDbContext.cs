using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence;

public class ShowManagementDbContext(DbContextOptions options) : DbContext(options), IShowManagementDbContext
{
    public DbSet<Show> Show { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowManagementDbContext).Assembly);
        modelBuilder.HasDefaultSchema("cms_show_management");
        base.OnModelCreating(modelBuilder);
    }
}