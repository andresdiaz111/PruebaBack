using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Dtos;

public class UserCreateDto
{
    [Required] public string? Email { get; set; }

    [Required] public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}