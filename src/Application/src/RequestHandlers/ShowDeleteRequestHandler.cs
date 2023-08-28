using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler : IRequestHandler<ShowDeleteRequest, bool>
{
    public Task<bool> Handle(ShowDeleteRequest request, CancellationToken cancellationToken)
    {
        // TODO: delete
        return Task.FromResult(true);
    }
}