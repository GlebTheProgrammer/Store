using System.Text.RegularExpressions;

namespace Store
{
    public class Book
    {
        public int Id { get; }

        public string Isbn { get; }

        public string Author { get; }

        public string Title { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Book(int id, string isbn, string author, string title, string description, decimal price)
        {
            Id = id;
            Isbn = isbn;
            Author = author;
            Title = title;
            Description = description;
            Price = price;
        }

        //Доступен всем классам внутри этого проекта и больше нигде, и через трюк, ещё и тестовому проекту
        internal static bool IsIsbn(string s)
        {
            if (s == null)
                return false;

            s = s.Replace("-", "")
                .Replace(" ", "")
                .ToUpper();
            // Возможно либо 10 цифр, либо 3 дополнительные. Запись ^ означает, что это обязательно должно быть началом строки, а $ - соответственно, концом
            return Regex.IsMatch(s, @"^ISBN\d{10}(\d{3})?$");  // Вызов метода, который возвращает true, если строка соответствует образцу паттерна; @ отключает экранирование
        }
    }
}
