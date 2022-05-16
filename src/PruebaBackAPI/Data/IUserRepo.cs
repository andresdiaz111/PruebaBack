using PruebaBackAPI.Models;

namespace PruebaBackAPI.Data;

public interface IUserRepo<T> where T : User
{
    Task<PaginationResult<T>> GetAllUsers(int page, int perPage);
    Task<T> GetUserById(int id);
    Task<T> CreateUser(T user);
    Task<T> UpdateUser(T user);
    bool SaveChanges();
}
