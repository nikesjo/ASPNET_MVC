using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers.Middlewares;

public class UserSessionValidationMiddleware
{
    private readonly RequestDelegate _next;

    public UserSessionValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
}
