using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        public IBooksRepository _repository;
        public BooksService(IBooksRepository repo)
        {
            _repository = repo;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _repository.Get();
        }

        public async Task<Guid> CreateBook(Book book)
        {
            return await _repository.Create(book);
        }

        public async Task<Guid> UpdateBook(Guid id, string title, string description, decimal price)
        {
            return await _repository.Update(id, title, description, price);

        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            return await _repository.Delete(id);
        }

    }
}
