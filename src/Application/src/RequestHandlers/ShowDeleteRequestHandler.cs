using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler : IRequestHandler<ShowDeleteRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserDeleteRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ShowDeleteRequest request, CancellationToken cancellationToken)
    {
        var toDelete = await _unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);
        if (toDelete is null)
        {
            return false;
        }

        //TODO: delete image

        _unitOfWork.ShowRepository.Delete(toDelete);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}