using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class ShowProfileImageUploadRequestHandler : IRequestHandler<ShowProfileImageUploadRequest, BaseCreateResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IStorageService _storageService;

    public ShowProfileImageUploadRequestHandler(
        IUnitOfWork unitOfWork,
        IStorageService storageService
    )
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<BaseCreateResponse?> Handle(ShowProfileImageUploadRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.ShowRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return null;

        if (string.IsNullOrEmpty(user.ProfileImageUrl) is false)
            await _storageService.DeleteProfileImage(user.ProfileImageUrl);

        (var id, user.ProfileImageUrl) = await _storageService.UploadProfileImage(request.Image);
        await _unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = id } ;
    }
}