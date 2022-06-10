using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PruebaBackAPI.Services;

[AttributeUsage(AttributeTargets.Method)]
public class Authorizer : Attribute, IAuthorizationFilter
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    public Authorizer(string clientId, string clientSecret)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    private bool EndPointAuth(string clientId, string clientSecret)
    {
        return clientId == _clientId && clientSecret == _clientSecret;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("client_secret", out var clientSecret);
        context.HttpContext.Request.Headers.TryGetValue("client_id", out var clientId);
        
        if(!EndPointAuth(clientId,clientSecret)) 
            context.Result = new UnauthorizedResult();
    }
}