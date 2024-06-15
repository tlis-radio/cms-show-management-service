using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonRequired]
    public string Name { get; set; } = null!;

    [JsonRequired]
    public string Description { get; set; } = null!;

    [JsonRequired]
    public Guid ProfileImageId { get; set; }

    [JsonRequired]
    public List<Guid> ModeratorIds { get; set; } = [];
}