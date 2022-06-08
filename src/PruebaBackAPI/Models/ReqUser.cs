using PruebaBackAPI.Dtos;

namespace PruebaBackAPI.Models;

public class ReqUser
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<UserCreateDto>? Data { get; set; }
    public Support? Support { get; set; }
}

public class Support
{
    public string? Url { get; set; }
    public string? Text { get; set; }
}