using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ShowManagement.Application.Mappers;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class ShowCreateRequestHandler(IUnitOfWork unitOfWork, ShowMapper mapper) : IRequestHandler<ShowCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(ShowCreateRequest request, CancellationToken cancellationToken)
    {   
        var toCreate = mapper.ToEntity(request);

        await unitOfWork.ShowRepository.InsertAsync(toCreate);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = toCreate.Id
        };
    }
}