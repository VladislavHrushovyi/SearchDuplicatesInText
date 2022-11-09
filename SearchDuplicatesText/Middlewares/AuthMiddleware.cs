namespace SearchDuplicatesText.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Token is null or empty");
        }
        var tokenResponse = await CheckToken(token);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Please login to system");
        }

        await AttachUserToContext(context, tokenResponse);
        await _next.Invoke(context);
    }

    private async Task AttachUserToContext(HttpContext context, HttpResponseMessage tokenResponse)
    {
        var content = await tokenResponse.Content.ReadAsStringAsync();
        context.Items.Add("User", content);
    }

    private async Task<HttpResponseMessage> CheckToken(string token)
    {
        var url = "https://localhost:7263/auth/jwt-isValid";
        var request = new HttpClient();
        request.DefaultRequestHeaders.Add("Authorization", token);

        var response = await request.GetAsync(url);

        return response;
    }
}