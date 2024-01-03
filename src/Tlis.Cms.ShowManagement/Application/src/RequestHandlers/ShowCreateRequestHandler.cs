using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowCreateRequestHandler : IRequestHandler<ShowCreateRequest, BaseCreateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ShowMapper _mapper;

    public ShowCreateRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseCreateResponse> Handle(ShowCreateRequest request, CancellationToken cancellationToken)
    {   
        var showToCreate = _mapper.ToEntity(request);

        await _unitOfWork.ShowRepository.InsertAsync(showToCreate);
        await _unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = showToCreate.Id
        };
    }
}