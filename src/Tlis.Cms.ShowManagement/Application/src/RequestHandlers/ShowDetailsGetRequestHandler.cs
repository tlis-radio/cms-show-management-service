using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowDetailsGetRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper)
    : IRequestHandler<ShowDetailsGetRequest, ShowDetailsGetResponse?>
{
    public async Task<ShowDetailsGetResponse?> Handle(ShowDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var show = await unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);

        return show is null ? null : mapper.ToDto(show);
    }
}