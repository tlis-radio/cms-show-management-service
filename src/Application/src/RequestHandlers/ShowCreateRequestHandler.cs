using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowCreateRequestHandler : IRequestHandler<ShowCreateRequest, BaseCreateResponse>
{
    private readonly ShowMapper _mapper;

    public ShowCreateRequestHandler(ShowMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<BaseCreateResponse> Handle(ShowCreateRequest request, CancellationToken cancellationToken)
    {
        var showToCreate = _mapper.ToEntity(request);

        // TODO: save to Db

        return Task.FromResult(new BaseCreateResponse
        {
            Id = showToCreate.Id
        });
    }
}