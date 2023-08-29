using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowDetailsGetRequestHandler : IRequestHandler<ShowDetailsGetRequest, ShowDetailsGetResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ShowMapper _mapper;

    public ShowDetailsGetRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShowDetailsGetResponse?> Handle(ShowDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var show = await _unitOfWork.ShowRepository.GetByIdAsync(request.Id, false);

        return show is null ? null : _mapper.ToDto(show);
    }
}