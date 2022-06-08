using System.ComponentModel.DataAnnotations;

namespace PruebaBackAPI.Models;

public class User
{
    [Key] [Required] public int id { get; set; }

    public string? email { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? avatar { get; set; }
}

public class PaginationResult<T>
{
    public long TotalCount { get; set; }
    public List<T>? Results { get; set; }
    public int ResultPerPage { get; set; }
    public long PageNumber { get; set; }
}