using Tlis.Cms.ShowManagement.Domain.Entities;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

namespace Tlis.Cms.ShowManagement.Infrastructure.Persistence.Repositories;

internal sealed class ShowRepository : GenericRepository<Show>, IShowRepository
{
    public ShowRepository(ShowManagementDbContext context) : base(context)
    {
    }
}