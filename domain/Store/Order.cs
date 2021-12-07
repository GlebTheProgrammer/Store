using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order // Сущность
    {
        public int Id { get; }

        private List<OrderItem> items;

         // Это как бы массив, только с ним ничего нельзя делать, нельзя менять, только читать
        public IReadOnlyCollection<OrderItem> Items  // Своего рода инкапсуляция, тк мы не можем менять это снаружи, но можем изнутри
        {
            get { return items; }
        }

        public int TotalCount
        {
            get { return items.Sum(item => item.Count); } // Просуммируем все количества и вернём их
        }

        public decimal TotalPrice
        {
            get { return items.Sum(item => item.Price * item.Count); }
        }

        public Order(int id, IEnumerable<OrderItem> items)  // IEnumerable - итератор. Мы не знаем, что туда придёт, но эта штука позволит перебирать все элементы коллекции
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items)); // nameof преобразует названи переменной в строку

            Id = id;

            this.items = new List<OrderItem>(items);
        }

        private void AddOrUpdateItem(Book book, int count)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var item = items.SingleOrDefault(x => x.BookId == book.Id);

            if (item == null)
            {
                items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else
            {
                items.Remove(item);
                items.Add(new OrderItem(book.Id, item.Count + count, book.Price));
            }
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            AddOrUpdateItem(book, 1);
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            AddOrUpdateItem(book, -1);
        }

        public void RemoveItem(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (items.Count == 0)
                throw new InvalidOperationException("Cart must contain items");

            var item = items.SingleOrDefault(x => x.BookId == book.Id);
            if (item == null)
                throw new InvalidOperationException("Cart does not contain item with ID: " + book.Id);

            items.RemoveAll(x => x.BookId == book.Id);
        }
    }
}
