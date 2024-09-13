using BookStore.Contracts;
using FluentValidation;

namespace BookStore.Core.Models
{
    public class BookValidator : IBookValidator
    {
        //public BookValidator()
        //{
        //    RuleFor(b => b.Title).NotEmpty().Length(1,25).WithMessage("Title cannot be empty or longer than 25 symbols");
        //    RuleFor(b => b.Description).Length(1, 300).WithMessage("Description cannot be empty or longer than 300 symbols");
        //    RuleFor(b => b.Price).NotEmpty().WithMessage("Price cannot be empty");
        //} 
        private int maxLengthTitle = 25;
        private int maxLengthDescr = 100;

        public bool IsValid(string title, string description, decimal price)
        {
            var titleLength = title.Length;
            if (titleLength > maxLengthTitle || titleLength == 0)
            {

                return false;
            }

            var descriptionLength = description.Length;
            if (descriptionLength > maxLengthDescr || descriptionLength == 0)
            {

                return false;
            }
            if (price == 0)
            {

                return false;
            }

            return true;
        }
    }



}
