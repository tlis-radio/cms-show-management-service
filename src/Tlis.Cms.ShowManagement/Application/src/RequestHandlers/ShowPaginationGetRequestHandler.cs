using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowPaginationGetRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper)
    : IRequestHandler<ShowPaginationGetRequest, PaginationResponse<ShowPaginationGetResponse>>
{
    public async Task<PaginationResponse<ShowPaginationGetResponse>> Handle(ShowPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var shows = await unitOfWork.ShowRepository.PaginationAsync(request.Limit, request.Page);

        return new PaginationResponse<ShowPaginationGetResponse>
        {
            Total = shows.Total,
            Limit = shows.Limit,
            Page = shows.Page,
            TotalPages = shows.TotalPages,
            Results = shows.Results.Select(mapper.ToPaginationDto).ToImmutableList()
        };
    }
}