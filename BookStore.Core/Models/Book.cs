using System;
using System.Net.WebSockets;

namespace BookStore.Core.Models
{
    public partial class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        private Book(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title= title;
            Description= description;
            Price= price;
            
        }
        public Guid Id { get;  }

        public string Title { get; }=string.Empty;

        public string Description { get; } = string.Empty;

        public decimal Price { get; }

        public static Book Create(Guid id, string title, string description, decimal price)
        {
            var book=new Book(id, title, description, price);

            return book;
        }

    }
}
