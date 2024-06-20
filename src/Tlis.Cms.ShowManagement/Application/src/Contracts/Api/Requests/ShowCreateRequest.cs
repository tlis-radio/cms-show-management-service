using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowCreateRequest : IRequest<BaseCreateResponse>
{
    [JsonRequired]
    public string Name { get; set; } = null!;

    [JsonRequired]
    public string Description { get; set; } = null!;

    [JsonRequired]
    public List<Guid> ModeratorIds { get; set; } = [];
}