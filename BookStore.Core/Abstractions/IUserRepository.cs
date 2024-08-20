using BookStore.Core.Enums;
using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid UserId);
    }
}