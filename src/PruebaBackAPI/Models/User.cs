using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Models;

public class User
{
    [Key]
    [Required]
    public int Id {get; set;}

    [Required]
    public string? Email {get; set;}

    [Required]
    public string? FirstName {get; set;}

    [Required]
    public string? LastName {get; set;}

    [Required]
    public string? Avatar {get; set;}
}

public class PaginationResult<T>
{
    public long TotalCount {get; set;}
    public List<T>? Results {get; set;}
    public int ResultPerPage {get; set;}
    public long PageNumber {get; set;}
}
