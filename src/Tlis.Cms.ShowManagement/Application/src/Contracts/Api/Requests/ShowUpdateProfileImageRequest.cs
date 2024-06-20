using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowUpdateProfileImageRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonRequired]
    public Guid ProfileImageId { get; set; }
}