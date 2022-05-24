namespace PruebaBackAPI.Dtos;
using System.ComponentModel.DataAnnotations;
public class UserDto
{
    [Required]
    public int Id {get; set;}
    [Required]
    public string? Email {get; set;}
    [Required]
    public string? First_name {get; set;}
    public string? Last_name {get; set;}
    public string? Avatar {get; set;}
}

public class PaginationDto<T>
{
    //public long TotalCount {get; set;}
    public List<T>? Results {get; set;}
    public int ResultPerPage {get; set;}
    public long PageNumber {get; set;}
}
