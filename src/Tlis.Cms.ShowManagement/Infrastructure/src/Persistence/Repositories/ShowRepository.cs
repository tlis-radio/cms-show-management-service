using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Dtos;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories;

internal sealed class ShowRepository(ShowManagementDbContext context)
    : GenericRepository<Show>(context), IShowRepository
{
    public async Task<PaginationDto<Show>> PaginationAsync(int limit, int pageNumber)
    {
        var queryGetTotalCount = await ConfigureTracking(DbSet.AsQueryable(), false).CountAsync();
        
        var pageQuery = ConfigureTracking(DbSet.AsQueryable(), false);

        var page = await pageQuery
            .OrderBy(u => u.CreatedDate)
            .Skip(limit * (pageNumber - 1))
            .Take(limit)
            .ToListAsync();
        
        return new PaginationDto<Show>
        {
            Total = queryGetTotalCount,
            Limit = limit,
            Page = pageNumber,
            Results = page
        };
    }
}