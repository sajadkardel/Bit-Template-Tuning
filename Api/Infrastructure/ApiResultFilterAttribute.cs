using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BTT.Api.Infrastructure;
public class ApiResultFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is not null)
        {
            var apiResult = new ApiResult<object>(true, HttpStatusCode.OK, objectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
        }
        else if(context.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
        {
            var apiResult = new ApiResult<object>(true, HttpStatusCode.OK, okObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
        }
        else if (context.Result is OkResult okResult)
        {
            var apiResult = new ApiResult(true, HttpStatusCode.OK);
            context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
        }

        base.OnResultExecuting(context);
    }
}