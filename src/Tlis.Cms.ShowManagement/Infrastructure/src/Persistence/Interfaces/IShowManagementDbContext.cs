using Microsoft.EntityFrameworkCore;
using Tlis.Cms.ShowManagement.Domain.Entities;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

public interface IShowManagementDbContext
{
    public DbSet<Show> Show { get; set; }

    public int SaveChanges();
}