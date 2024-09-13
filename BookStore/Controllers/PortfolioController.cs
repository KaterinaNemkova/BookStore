using BookStore.Application;
using BookStore.Application.Services;
using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PortfolioController:ControllerBase
    {
        private readonly IPortfolioRepository _repo;
        

        public PortfolioController(IPortfolioRepository repository)
        {
            _repo = repository;
           
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<List<Book>>> ShowMyPortfolios()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var portfolios = await _repo.GetMyPortfolio(userId);

            return Ok(portfolios);
        }

        [HttpPost("{bookId:guid}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> AddPortfolio(Guid bookId)

        {
            var checkBook = await _repo.AlreadyExist(bookId);

            if (checkBook == true)
            {
                return BadRequest("This book is already in your portfolio");
            }
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

           

            var portfolio = new Portfolio
            {
                UserId = userId,
                BookId = bookId,
            };
            await _repo.Add(portfolio);

            return Ok(portfolio);
        }

        [HttpDelete("{bookId:guid}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeletePortfolio(Guid bookId)
        {
            var checkBook = await _repo.AlreadyExist(bookId);

            if (checkBook == false)
            {
                return BadRequest("This book is already deleted");
            }
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var portfolio = new Portfolio
            {
                UserId = userId,
                BookId = bookId,
            };

            await _repo.DeletePortfolio(portfolio);
            return Ok();

        }

    }
}
