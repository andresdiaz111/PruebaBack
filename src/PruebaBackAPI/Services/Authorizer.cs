using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PruebaBackAPI.Services;

[AttributeUsage(AttributeTargets.Method)]
public class Authorizer : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("client_secret", out var clientSecret);
        context.HttpContext.Request.Headers.TryGetValue("client_id", out var clientId);

        if (!EndPointAuth(clientId, clientSecret))
            context.Result = new UnauthorizedResult();
    }

    private static bool EndPointAuth(string clientId, string clientSecret)
    {
        return clientId == Program.Config.GetSection("AuthConfig")["ClientId"] &&
               clientSecret == Program.Config.GetSection("AuthConfig")["ClientSecret"];
    }
}