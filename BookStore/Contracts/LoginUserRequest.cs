using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record LoginUserRequest(
    string email,
    string password);
}
