using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record RegisterUserRequest(
     [Required] string UserName,
     [Required] string Password,
     [Required] string Email);
}
