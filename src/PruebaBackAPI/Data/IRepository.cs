using PruebaBackAPI.Models;

namespace PruebaBackAPI.Data;

public interface IRepository<T>
{
    Task<PaginationResult<T>> GetAll(int page, int perPage);
    Task<T> GetById(int id);
    Task Create(T user);
    Task<bool> SaveChanges();
}