using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

public class ShowDetailsGetResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<ShowDetailsGetResponseModerators> Moderators { get; set; } = [];

    public DateOnly CreatedDate { get; set; }

    public ShowDetailsGetResponseImage? ProfileImage { get; set; }
}