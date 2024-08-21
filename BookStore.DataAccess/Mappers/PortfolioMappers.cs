using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Mappers
{
    public static class PortfolioMappers
    {
        public static Portfolio ToPortfolio(PortfolioEntity entity)
        {
            var portfolio = new Portfolio
            {
                UserId = entity.UserId,
                BookId = entity.BookId,
                //book = Book.Create(entity.Book.Id, entity.Book.Title, entity.Book.Description, entity.Book.Price).book

            };

            return portfolio;
        }
    }
}
