using System;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowDetailsGetRequest : IRequest<ShowDetailsGetResponse>
{
    public Guid Id { get; set; }
}