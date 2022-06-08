using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Dtos;

public class UserCreateDto
{
    [Required] public string? Email { get; set; }

    [Required] public string? First_name { get; set; }

    public string? Last_name { get; set; }
    public string? Avatar { get; set; }
}