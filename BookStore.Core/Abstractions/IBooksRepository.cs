﻿using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<bool> AlreadyExist(Guid bookId);
        Task<Guid> Create(Book book);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}