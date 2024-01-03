using System.Threading.Tasks;
using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IShowRepository : IGenericRepository<Show>
{
    Task<PaginationDto<Show>> PaginationAsync(int limit, int pageNumber);
}