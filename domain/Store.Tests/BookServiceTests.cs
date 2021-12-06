using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class BookServiceTests
    {
        //[Fact]
        //public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        //{
        //    var bookRepositoryStub = new Mock<IBookRepository>();

        //    // Mock при помощи заглушки создаёт объект IBookRepository. Мы говорим библиотеке, что если будет вызываться метод этого объекта
        //    // GetAllByIsbn с любым строковым параметром, ты верни новый массив книжек

        //    bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
        //        .Returns(new[] { new Book(1, "", "", "") });
        //    bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
        //        .Returns(new[] { new Book(2, "", "", "") });

        //    var bookService = new BookService(bookRepositoryStub.Object); // Создаём объект, который при обращении вернёт то, что мы указали сверху
        //    var invalidAsbn = "ISBN 12345-67890";

        //    // Если мы передали правильный ISBN и вызвали метод GetAllByQuery, он обратитя к нашему bookRepositoryStub, и вызовет метод GetAllByIsbn
        //    // который мы прописали выше как заглушку, если всё правильно написано, то идентификатор у книги должен прийти как 1
        //    var actual = bookService.GetAllByQuery(invalidAsbn);

            
        //    Assert.Collection(actual, book => Assert.Equal(1, book.Id));  // Ожидаемый параметр - 1. Фактический - тот, которы й пришёл от book.Id
        //}

        [Fact]
        public void GetAllByQuery_WithAuthor_CallsGetAllByTitleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            
            bookRepositoryStub.Setup(x => x.GetAllByIsbn("Ritchie"))
                .Returns(new[] { new Book(1, "", "", "", "", 0m) });
            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor("Ritchie"))
                .Returns(new[] { new Book(2, "", "", "", "",0m) });

            var bookService = new BookService(bookRepositoryStub.Object); 
            var author = "Ritchie";

            
            var actual = bookService.GetAllByQuery(author);

         
            Assert.Collection(actual, book => Assert.Equal(2, book.Id));  
        }

        // Заглушка для bookRepository

        // Что тестируем_с каким значением_что должно вернуться
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            const int idOfIsbnSearch = 1;
            const int idOfAuthorSearch = 2;

            var bookRepository = new StubBookRepository();

            bookRepository.ResultOfGetAllByIsbn = new[]
            {
                new Book(idOfIsbnSearch,"","","","",0m)
            };

            bookRepository.ResultOfGetAllByTitleOrAuthor = new[]
            {
                new Book(idOfAuthorSearch,"","","","",0m)
            };

            var bookService = new BookService(bookRepository);

            var books = bookService.GetAllByQuery("ISBN 12345-67890");


            Assert.Collection(books, book => Assert.Equal(idOfIsbnSearch, book.Id));

        }

        [Fact]
        public void GetAllByQuery_WithTitle_CallsGetAllByTitleOrAuthor()
        {
            const int idOfIsbnSearch = 1;
            const int idOfAuthorSearch = 2;

            var bookRepository = new StubBookRepository();

            bookRepository.ResultOfGetAllByIsbn = new[]
            {
                new Book(idOfIsbnSearch,"","","","",0m)
            };

            bookRepository.ResultOfGetAllByTitleOrAuthor = new[]
            {
                new Book(idOfAuthorSearch,"","","","",0m)
            };

            var bookService = new BookService(bookRepository);

            var books = bookService.GetAllByQuery("Programming");


            Assert.Collection(books, book => Assert.Equal(idOfAuthorSearch, book.Id));

        }
    }
}
