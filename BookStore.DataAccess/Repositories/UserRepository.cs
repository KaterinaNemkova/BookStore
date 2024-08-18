using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using BookStore.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreDbContext _context;
        


        public UserRepository(BookStoreDbContext context)
        {
            _context = context;
            
        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email

            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            return UserMappers.ToUser(userEntity);

        }


    }
}
