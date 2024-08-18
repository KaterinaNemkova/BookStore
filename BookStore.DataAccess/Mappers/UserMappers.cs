using BookStore.Core.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.Mappers
{
    public static class UserMappers
    {
        public static User ToUser(UserEntity entity)
        {
            var user = User.Create(entity.Id, entity.UserName, entity.PasswordHash, entity.Email);

            return user;
        }
    }
}
