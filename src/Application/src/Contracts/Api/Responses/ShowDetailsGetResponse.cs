using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowDetailsGetResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public IList<Guid> ModeratorIds { get; set; } = new List<Guid>();
}