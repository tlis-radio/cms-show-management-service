using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tlis.Cms.ShowManagement.Api.Controllers.Attributes;

public class FormFileContentTypeFilterAttribute : ActionFilterAttribute
{
    public string? ContentType { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (string.IsNullOrWhiteSpace(ContentType)) return;

        var contentTypes = ContentType.Split(",");

        var correctFileFormat = filterContext.HttpContext.Request.Form.Files.Any(file => contentTypes.Contains(file.ContentType));

        if (correctFileFormat is false)
        {
            filterContext.Result = new BadRequestResult();
        }
    }
}