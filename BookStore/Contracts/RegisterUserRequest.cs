using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record RegisterUserRequest(
     string UserName,
     string Password,
     string Email);
}
