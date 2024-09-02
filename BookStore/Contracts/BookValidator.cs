using BookStore.Contracts;
using FluentValidation;

namespace BookStore.Core.Models
{
        public class BookValidator : AbstractValidator<BooksRequest>
        {
            public BookValidator()
            {
                RuleFor(b => b.Title).NotEmpty().Length(1,25).WithMessage("Title cannot be empty or longer than 25 symbols");
                RuleFor(b => b.Description).Length(1, 300).WithMessage("Description cannot be empty or longer than 300 symbols");
                RuleFor(b => b.Price).NotEmpty().WithMessage("Price cannot be empty");
            }  
        }


    
}
