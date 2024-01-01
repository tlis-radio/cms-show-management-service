using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;

public sealed class ShowCreateRequest : IRequest<BaseCreateResponse>
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public IList<Guid> ModeratorIds { get; set; } = new List<Guid>();
}