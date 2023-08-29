using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ShowManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler : IRequestHandler<ShowUpdateRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ShowUpdateRequest request, CancellationToken cancellationToken)
    {
        var toUpdate = await _unitOfWork.ShowRepository.GetByIdAsync(request.Id, true);
        if (toUpdate is null)
        {
            return false;
        }

        toUpdate.Name = request.Name;
        toUpdate.Description = request.Description;

        toUpdate.ModeratorIds.Clear();
        toUpdate.ModeratorIds.AddRange(request.ModeratorIds);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}