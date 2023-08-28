using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowDetailsGetRequestHandler : IRequestHandler<ShowDetailsGetRequest, ShowDetailsGetResponse>
{
    private readonly ShowMapper _mapper;

    public ShowDetailsGetRequestHandler(ShowMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<ShowDetailsGetResponse> Handle(ShowDetailsGetRequest request, CancellationToken cancellationToken)
    {
        // TODO: get from Db

        return Task.FromResult(new ShowDetailsGetResponse());
    }
}