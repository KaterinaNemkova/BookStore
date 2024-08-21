using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly BookStoreDbContext _context;
        public PortfolioRepository(BookStoreDbContext context)
        {
            _context = context;

        }

        public async Task<Portfolio> Add(Portfolio portfolio)
        {
            var portfolioEntity = new PortfolioEntity
            {
                UserId = portfolio.UserId,
                BookId = portfolio.BookId
            };
            await _context.Portfolios.AddAsync(portfolioEntity);
            await _context.SaveChangesAsync();
           return PortfolioMappers.ToPortfolio(portfolioEntity);

        }
        public async Task<List<Book>> GetMyPortfolio(Guid userId)
        {
            var bookIds = await _context.Portfolios
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Select(p => p.BookId)
                .ToListAsync();

            if (bookIds.Count == 0)
            {
                return new List<Book>(); // Возвращаем пустой список, если нет книг для данного пользователя
            }

            // Загружаем книги по списку BookId
            var bookEntities = await _context.Books
                .AsNoTracking()
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            // Преобразуем сущности книг в объекты Book
            var books = bookEntities.Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).book).ToList();

            return books;

        }

        public async Task<Portfolio> DeletePortfolio(Portfolio portfolio)
        {
            var portfolioEntity = await _context.Portfolios.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == portfolio.UserId && u.BookId == portfolio.BookId);

            if (portfolioEntity == null)
            {
                return null;
            }

            _context.Remove(portfolioEntity);
            await _context.SaveChangesAsync();
            return PortfolioMappers.ToPortfolio(portfolioEntity);
        }

        
    }
}
