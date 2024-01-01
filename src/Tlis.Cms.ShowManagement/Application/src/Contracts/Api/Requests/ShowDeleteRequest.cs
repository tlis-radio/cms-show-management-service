using System;
using MediatR;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowDeleteRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}