using BookStore.Application;
using BookStore.Contracts;
using BookStore.Controllers;
using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBooksService> _booksServiceMock;
        private readonly Mock<IBookValidator> _bookValidatorMock;
        private readonly BooksController _booksController;

        public BookControllerTests()
        {
            _booksServiceMock = new Mock<IBooksService>();
            _bookValidatorMock = new Mock<IBookValidator>();
            _booksController = new BooksController(_booksServiceMock.Object, _bookValidatorMock.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            var book1 = Book.Create(Guid.NewGuid(),"Book 1",  "Description 1", 100);
            var book2 = Book.Create(Guid.NewGuid(), "Book 2", "Description 2", 200);
            // Arrange
            var books = new List<Book>
            {
                book1,
                book2
            };

            _booksServiceMock.Setup(service => service.GetAllBooks())
                    .ReturnsAsync(books);
            //Act
            var result = await _booksController.GetBooks();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedBooks= Assert.IsType<List<Book>>(okResult.Value);

            Assert.Equal(2, returnedBooks.Count);

            for (int i = 0; i < books.Count; i++)
            {
                Assert.Equal(books[i].Id, returnedBooks[i].Id);
                Assert.Equal(books[i].Title, returnedBooks[i].Title);
                Assert.Equal(books[i].Description, returnedBooks[i].Description);
                Assert.Equal(books[i].Price, returnedBooks[i].Price);
            }

        }

        [Fact]
        public async Task Create_books_succesfully()
        {
            var request = new BooksRequest("book3", "descr3", 45);

            _bookValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);

            _booksServiceMock.Setup(service => service.CreateBook(It.IsAny<Book>())).ReturnsAsync(Guid.NewGuid());

            var result=await _booksController.CreateBook(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.IsType<Guid>(okResult.Value);
        }
        [Fact]
        public async Task Create_books_Failed()
        {
            var request = new BooksRequest("book3", "descr3", 0);

            _bookValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>())).Returns(false);

            var result = await _booksController.CreateBook(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Book is invalid", badRequestResult.Value);
        }

        [Fact]
        public async Task Update_Book_

   


    }
}
