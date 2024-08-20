using BookStore.Application;
using BookStore.Contracts;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;
        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy ="User")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _service.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));
            return Ok(books);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var bookId = await _service.CreateBook(book);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> UpdateBook([FromBody] BooksRequest request, Guid id)
        {
            var bookId = await _service.UpdateBook(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var Id=await _service.DeleteBook(id);

            return Ok(Id);
        }


    }
}
