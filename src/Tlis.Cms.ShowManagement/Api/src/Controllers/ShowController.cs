using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.ShowManagement.Api.Constants;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ShowManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ShowManagement.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public sealed class ShowController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ShowDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Get show's details")]
    public ValueTask<ActionResult<ShowDetailsGetResponse>> GetShowDetails([FromRoute] Guid id)
        => HandleGet(new ShowDetailsGetRequest { Id = id });

    [HttpGet("pagination")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginationResponse<ShowPaginationGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Paging show's")]
    public ValueTask<ActionResult<PaginationResponse<ShowPaginationGetResponse>>> Pagination([FromQuery] ShowPaginationGetRequest request)
        => HandleGet(request);

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
}