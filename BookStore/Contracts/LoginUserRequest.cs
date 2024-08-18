using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record LoginUserRequest(
    [Required] string email,
    [Required] string password);
}
