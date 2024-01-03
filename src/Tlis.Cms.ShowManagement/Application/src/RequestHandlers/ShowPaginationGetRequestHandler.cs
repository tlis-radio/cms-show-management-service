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

internal sealed class ShowPaginationGetRequestHandler : IRequestHandler<ShowPaginationGetRequest, PaginationResponse<ShowPaginationGetResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ShowMapper _mapper;

    public ShowPaginationGetRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ShowPaginationGetResponse>> Handle(ShowPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.ShowRepository.PaginationAsync(request.Limit, request.Page);

        return new PaginationResponse<ShowPaginationGetResponse>
        {
            Total = users.Total,
            Limit = users.Limit,
            Page = users.Page,
            TotalPages = users.TotalPages,
            Results = users.Results.Select(_mapper.ToPaginationDto).ToImmutableList()
        };
    }
}