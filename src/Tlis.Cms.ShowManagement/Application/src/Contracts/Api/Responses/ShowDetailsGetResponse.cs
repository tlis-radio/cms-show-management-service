using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowDetailsGetResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public IList<Guid> ModeratorIds { get; set; } = new List<Guid>();

    public DateOnly CreatedDate { get; set; }

    public string? ProfileImageUrl { get; set; }
}