using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowUpdateProfileImageRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ShowUpdateProfileImageRequest, bool>
{
    public async Task<bool> Handle(ShowUpdateProfileImageRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.ProfileImageId = request.ProfileImageId;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}