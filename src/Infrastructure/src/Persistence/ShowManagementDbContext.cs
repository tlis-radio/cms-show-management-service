using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence;

public class ShowManagementDbContext : DbContext, IShowManagementDbContext
{
    public IShowRepository ShowRepository => _lazyShowRepository.Value;

    public DbSet<Show> Show { get; set; } = null!;

    private readonly Lazy<IShowRepository> _lazyShowRepository;

    private readonly ShowManagementDbContext _dbContext;

    public ShowManagementDbContext(ShowManagementDbContext dbContext, DbContextOptions options)
        : base(options)
    {
        _dbContext = dbContext;
        _lazyShowRepository = new(() => new ShowRepository(_dbContext));
    }

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