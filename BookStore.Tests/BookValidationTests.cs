using BookStore.Core.Models;


namespace BookStore.Tests
{
    public class BookValidationTests
    {
        private readonly BookValidator _validator;

        public BookValidationTests()
        {
            _validator = new BookValidator();
        }
        [Fact]
        public void IsValid_Book_ReturnTrue()
        {
            Assert.True(_validator.IsValid("��� �����", "������ �������", 56));

        }
        
        [Theory]
        [InlineData("��� �����", "������ �������", 0)]
        [InlineData("��� �����kmawkfmammmvm;lalmc;ladmc;lak,d", "������ �������", 34)]
        public void IsValid_Book_ReturnFalse(string title, string descr, decimal price)
        {
            Assert.False(_validator.IsValid(title, descr,price));
        }
    }
}