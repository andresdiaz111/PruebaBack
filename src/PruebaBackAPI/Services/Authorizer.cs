namespace PruebaBackAPI.Services;

public class Authorizer
{
    private readonly IConfiguration _config;

    public Authorizer(IConfiguration config)
    {
        _config = config;
    }

    public bool EndPointAuth(string client_id, string client_secret)
    {
        var id = _config["client_id"];
        var secret = _config["client_secret"];

        if (client_id == id && client_secret == secret)
            return true;

        return false;
    }
}