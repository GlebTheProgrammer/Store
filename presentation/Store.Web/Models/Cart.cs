using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class Cart
    {
        // У нашей корзины есть свойство Items - это словарь, у которого ключи и значения - это целые числа. Создаём его пустым изначально
        public IDictionary<int, int> Items { get; set; } = new Dictionary<int, int>();

        public decimal Amount { get; set; }
    }
}
