using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler : IRequestHandler<ShowUpdateRequest, bool>
{
    public Task<bool> Handle(ShowUpdateRequest request, CancellationToken cancellationToken)
    {
        // get from db

        // update properties

        // TODO: save to Db
        return Task.FromResult(true);
    }
}