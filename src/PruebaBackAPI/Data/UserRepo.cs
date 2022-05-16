using PruebaBackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PruebaBackAPI.Data;

public class UserRepo<T> : IUserRepo<T> where T : User
{
    private readonly DbSet<T>? _entities;
    private readonly UserContext _context;

    public UserRepo(UserContext context)
    {
        _entities = context.Set<T>();
        _context = context;
    }
    public Task<T> CreateUser(T user)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResult<T>> GetAllUsers(int page, int perPage)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateUser(T user)
    {
        throw new NotImplementedException();
    }
}
