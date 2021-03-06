using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Dtos;

public class UserDto
{
    [Required] public int Id { get; set; }

    [Required] public string? Email { get; set; }
    [Required] public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}

public class PaginationDto<T>
{
    //public long TotalCount {get; set;}
    public List<T>? Results { get; set; }
    public int ResultPerPage { get; set; }
    public long PageNumber { get; set; }
}