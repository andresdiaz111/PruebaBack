using Microsoft.EntityFrameworkCore;
using PruebaBackAPI.Models;

namespace PruebaBackAPI.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<User>? Users { get; set; }
}