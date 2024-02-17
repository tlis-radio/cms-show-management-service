using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ShowUpdateRequest, bool>
{
    public async Task<bool> Handle(ShowUpdateRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;

        toUpdate.ModeratorIds.Clear();
        toUpdate.ModeratorIds.AddRange(request.ModeratorIds);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}