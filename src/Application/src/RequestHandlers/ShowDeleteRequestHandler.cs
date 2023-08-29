using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler : IRequestHandler<ShowDeleteRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IStorageService _storageService;

    public UserDeleteRequestHandler(IUnitOfWork unitOfWork, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<bool> Handle(ShowDeleteRequest request, CancellationToken cancellationToken)
    {
        var toDelete = await _unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);
        if (toDelete is null)
        {
            return false;
        }

        _unitOfWork.ShowRepository.Delete(toDelete);
        await _unitOfWork.SaveChangesAsync();

        if (string.IsNullOrEmpty(toDelete.ProfileImageUrl) is false)
            await _storageService.DeleteProfileImage(toDelete.ProfileImageUrl);

        return true;
    }
}