using System.Text.Json.Serialization;

namespace PruebaBackAPI.Dtos;

public class UserUpdateDto
{
    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("first_name")] public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Avatar { get; set; }
}