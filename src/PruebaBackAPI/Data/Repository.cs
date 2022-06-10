using Microsoft.EntityFrameworkCore;
using PruebaBackAPI.Models;

namespace PruebaBackAPI.Data;

public class Repository<T> : IRepository<T> where T : User
{
    private readonly UserContext _context;
    private readonly DbSet<T> _entities;

    public Repository(UserContext context)
    {
        _entities = context.Set<T>();
        _context = context;
    }

    public async Task Create(T user)
    {
        ArgumentNullException.ThrowIfNull(user);

        await _context.Users!.AddAsync(user);
    }

    public async Task<PaginationResult<T>> GetAll(int page, int perPage)
    {
        var count = await _entities.CountAsync();
        var entsToSkip = (page - 1) * perPage;
        var entities = await _entities.OrderBy(ent => ent.id).Skip(entsToSkip).Take(perPage).ToListAsync();

        return new PaginationResult<T>
        {
            TotalCount = count,
            Results = entities,
            ResultPerPage = perPage,
            PageNumber = page
        };
    }

    public async Task<T> GetById(int id)
    {
        return (await _entities.SingleOrDefaultAsync(ent => ent.id == id))!;
    }

    public async Task<bool> SaveChanges()
    {
        var save = await _context.SaveChangesAsync();

        return save >= 0;
    }
}