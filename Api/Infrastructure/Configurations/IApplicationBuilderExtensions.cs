using BTT.Api.Infrastructure.Middlewares;

namespace BTT.Api.Infrastructure.Configurations;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseHttpResponseExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpResponseExceptionHandlerMiddleware>();
    }
}
