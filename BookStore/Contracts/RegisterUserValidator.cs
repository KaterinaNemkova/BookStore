using FluentValidation;

namespace BookStore.Contracts
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(1,20).WithMessage("User cannot be empty or longer than 20 symbols");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password cannot be empty or must be at least 6 characters long.");
        }
    }
}
