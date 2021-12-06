using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 02011038013", "D. Knuth", "Art Of Programming, Vol. 1","This volume begins with basic programming" +
                " concepts and techniques, then focuses more particularly on information structures - the representation of information" +
                " inside a computer, the structural relationship between data elements and how to deal with them efficiently.",7.19m),


            new Book(2, "ISBN 0201485672", "M. Fowler", "Refactoring","Describes the objects technologies",12.45m),

            new Book(3, "ISBN 0131101633", "B. W. Kernighan, D. M. Ritchie", "C Programming Language","Known as a bible of C",14.98m),
         };

        public Book[] GetAllByIsbn(string isbn)
        {
            // Через язык запросов Linq отбираем из массива только нужные нам книги
            return books.Where(book => book.Isbn == isbn)
                .ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Author.Contains(query) || book.Title.Contains(query))
                .ToArray();
        }

        public Book GetById(int id)
        {
            // Вызываем метод Single,и он должен сработать только для одной книги с нужным нам идентификатором.
            // Если ничего не нашли/нашли 2 книги, выкидываем исключение, тк мы не можем вернуть null
            return books.Single(book => book.Id == id); 
        }
    }
}
