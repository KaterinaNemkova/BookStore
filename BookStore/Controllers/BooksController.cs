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
        private readonly IBookValidator _validator;
        public BooksController(IBooksService service,IBookValidator validator)
        {
            _service = service;
            _validator = validator;
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
        public async Task<ActionResult<Book>> CreateBook([FromBody] BooksRequest request)
        {
            if (!_validator.IsValid(request.Title,request.Description,request.Price))
            {
                return BadRequest("Book is invalid");
            }
            var book = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            
            var bookId = await _service.CreateBook(book);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> UpdateBook([FromBody] BooksRequest request, Guid id)
        {
            bool existBook=await _service.AlreadyExistBook(id);

            if (existBook == false)
            {
                return NotFound("This book doesn't exist");
            }
            if (!_validator.IsValid(request.Title, request.Description, request.Price))
            {
                return BadRequest("Book is invalid");
            }
            var bookId = await _service.UpdateBook(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var ExistBook=await _service.AlreadyExistBook(id);

            if (ExistBook == false)
            {
                return NotFound("Book is already deleted");
            }
           var Id= await _service.DeleteBook(id);

            return Ok(Id);
        }


    }
}
