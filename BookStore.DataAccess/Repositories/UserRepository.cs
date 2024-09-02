using BookStore.Core.Enums;
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
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin)
                ??throw new InvalidOperationException();

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Roles = {roleEntity}
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception(); 

            return UserMappers.ToUser(userEntity);
        }

        public async Task<HashSet<Permission>> GetUserPermissions(Guid UserId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions)
                .Where(u => u.Id == UserId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }


    }
}
