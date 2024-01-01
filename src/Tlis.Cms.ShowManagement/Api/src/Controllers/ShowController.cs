using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.ShowManagement.Api.Constants;
using Tlis.Cms.ShowManagement.Api.Controllers.Attributes;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public sealed class ShowController : BaseController
{
    public ShowController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy.ShowRead)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ShowDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Get show's details")]
    public ValueTask<ActionResult<ShowDetailsGetResponse>> GetShowDetails([FromRoute] Guid id)
        => HandleGet(new ShowDetailsGetRequest { Id = id });

    [HttpPost]
    [Authorize(Policy.ShowWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create show")]
    public ValueTask<ActionResult<BaseCreateResponse>> CreateShow([FromBody, Required] ShowCreateRequest request)
        => HandlePost(request);

    [HttpPut("{id:guid}")]
    [Authorize(Policy.ShowWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update show's details")]
    public ValueTask<ActionResult> UpdateShow([FromRoute] Guid id, [FromBody, Required] ShowUpdateRequest request)
    {
        request.Id = id;

        return HandlePut(request);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.ShowDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete show")]
    public ValueTask<ActionResult> DeleteShow([FromRoute] Guid id)
        => HandleDelete(new ShowDeleteRequest { Id = id });

    [HttpPost("{id:guid}/profile-image")]
    [Authorize(Policy.ShowWrite)]
    [RequestSizeLimit(5000000)]
    [FormFileContentTypeFilter(ContentType = "image/jpeg,image/png")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Upload show's profile image.",
        "If user already has an image current profile image will be deleted and replaced with this new image. Maximal allowed size is 5 Megabyte.")]
    public ValueTask<ActionResult<BaseCreateResponse>> UploadProfileImage([FromRoute] Guid id, IFormFile image)
        => HandlePost(new ShowProfileImageUploadRequest { Id = id, Image = image });
}