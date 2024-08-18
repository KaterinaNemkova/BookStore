
namespace BookStore.Application.Services
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string username, string password, string email);
    }
}