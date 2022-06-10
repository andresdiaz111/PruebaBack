using System.Text.Json.Serialization;

namespace PruebaBackAPI.Dtos;

public class UserUpdateDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("first_name")]
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Avatar { get; set; }
}