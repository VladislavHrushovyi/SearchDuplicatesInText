namespace SearchDuplicatesText.Middlewares;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<AuthMiddleware>();
        return builder;
    }
}