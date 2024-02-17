using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ShowDeleteRequest, bool>
{
    public async Task<bool> Handle(ShowDeleteRequest request, CancellationToken cancellationToken)
    {
        var toDelete = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);
        if (toDelete is null)
        {
            return false;
        }

        unitOfWork.ShowRepository.Delete(toDelete);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}