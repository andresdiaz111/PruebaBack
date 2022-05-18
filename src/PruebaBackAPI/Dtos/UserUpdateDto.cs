using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Dtos;

public class UserUpdateDto
{
    public string? Email {get; set;}
    public string? First_name {get; set;}
    public string? Last_name {get; set;}
    public string? Avatar {get; set;}
}
