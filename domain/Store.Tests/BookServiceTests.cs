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
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            // Mock при помощи заглушки создаёт объект IBookRepository. Мы говорим библиотеке, что если будет вызываться метод этого объекта
            // GetAllByIsbn с любым строковым параметром, ты верни новый массив книжек

            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object); // Создаём объект, который при обращении вернёт то, что мы указали сверху
            var invalidAsbn = "ISBN 12345-67890";

            // Если мы передали правильный ISBN и вызвали метод GetAllByQuery, он обратитя к нашему bookRepositoryStub, и вызовет метод GetAllByIsbn
            // который мы прописали выше как заглушку, если всё правильно написано, то идентификатор у книги должен прийти как 1
            var actual = bookService.GetAllByQuery(invalidAsbn);

            
            Assert.Collection(actual, book => Assert.Equal(1, book.Id));  // Ожидаемый параметр - 1. Фактический - тот, которы й пришёл от book.Id
        }

        [Fact]
        public void GetAllByQuery_WithAuthor_CallsGetAllByTitleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object); 
            var validIsbn = "12345-67890";

            
            var actual = bookService.GetAllByQuery(validIsbn);

         
            Assert.Collection(actual, book => Assert.Equal(2, book.Id));  
        }
    }
}
