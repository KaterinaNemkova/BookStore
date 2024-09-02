using BookStore.Contracts;
using FluentValidation;


namespace BookStore.Core.Models
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator() 
        {
            
            RuleFor(x=>x.email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
