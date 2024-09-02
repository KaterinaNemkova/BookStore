using BookStore.Core.Models;


namespace BookStore.DataAccess.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> Add(Portfolio portfolio);
        Task<bool> AlreadyExist(Guid bookId);
        Task<Portfolio> DeletePortfolio(Portfolio portfolio);
        Task<List<Book>> GetMyPortfolio(Guid userId);
    }
}