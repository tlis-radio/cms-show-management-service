using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowPaginationGetResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<Guid> ModeratorIds { get; set; } = [];

    public DateOnly CreatedDate { get; set; }

    public Guid? ProfileImageId { get; set; }
}