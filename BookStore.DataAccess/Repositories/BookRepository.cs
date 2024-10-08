﻿using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        public readonly BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var booksEntities = await _context.Books.AsNoTracking().ToListAsync();

            var books = booksEntities
                .Select(b => Book.
                Create(b.Id, b.Title, b.Description, b.Price))
                .ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Books
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price)
                );
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<bool> AlreadyExist(Guid bookId)
        {
            var bookExist = await _context.Books.AnyAsync(x => x.Id == bookId);

            return bookExist;

        }


    }
}
