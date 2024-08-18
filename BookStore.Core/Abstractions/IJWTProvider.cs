using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}