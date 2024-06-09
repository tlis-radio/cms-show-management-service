using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowDetailsGetRequestHandler(
    IUnitOfWork unitOfWork,
    IUserManagementHttpService userManagementHttpService,
    IImageManagementHttpService imageManagementHttpService,
    ShowMapper mapper)
    : IRequestHandler<ShowDetailsGetRequest, ShowDetailsGetResponse?>
{
    public async Task<ShowDetailsGetResponse?> Handle(ShowDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);

        if (show is null)
        {
            return null;
        }

        var userFilterResponse = await userManagementHttpService.FilterUsersAsync(show.ModeratorIds);

        var image = show.ProfileImageId.HasValue
            ? await imageManagementHttpService.GetImageAsync(show.ProfileImageId.Value)
            : null;

        return mapper.ToDto(show, userFilterResponse.Results, image);
    }
}