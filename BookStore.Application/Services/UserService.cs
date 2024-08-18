using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTProvider _jwtProvider;
        public UserService(IUserRepository repository, IPasswordHasher hasher, IJWTProvider jwtProvider)
        {
            _repository = repository;
            _passwordHasher = hasher;
            _jwtProvider = jwtProvider;

        }
        public async Task Register(string username, string password, string email)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user=User.Create(Guid.NewGuid(),username, hashedPassword, email);

            await _repository.Add(user);
        }

        public async Task<string> Login(string email,string password)
        {
            var user= await _repository.GetByEmail(email);

            var result= _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                Console.WriteLine("Not right password");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;


        }
    }
}
