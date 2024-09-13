namespace BookStore.Core.Models
{
    public interface IBookValidator
    {
        bool IsValid(string title, string description, decimal price);
    }
}