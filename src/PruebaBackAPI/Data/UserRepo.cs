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
    public async Task CreateUser(T user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _context.Users.AddAsync(user);
    }

    public async Task<PaginationResult<T>> GetAllUsers(int page, int perPage)
    {
        var count = await _entities.CountAsync();
        var entsToSkip = (page - 1) * perPage;
        var entities = await _entities.OrderBy(ent => ent.id).Skip(entsToSkip).Take(perPage).ToListAsync();
        return new PaginationResult<T>
        {
            TotalCount = count,
            Results = entities,
            ResultPerPage = perPage,
            PageNumber = page,
        };
    }

    public async Task<T> GetUserById(int id)
    {
        return await _entities.SingleOrDefaultAsync(ent => ent.id == id);
    }

    public async Task<bool> SaveChanges()
    {
        var save = await _context.SaveChangesAsync();

        return (save >= 0);
    }

    public void UpdateUser(T user)
    {
        //Prueba
    }
}
