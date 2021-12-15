using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class Order // Сущность
    {
        public int Id { get; }

        public OrderItemCollection Items { get; }

        public string CellPhone { get; set; }

        public OrderDelivery Delivery { get; set; }

        public OrderPayment Payment { get; set; }

        public int TotalCount => Items.Sum(item => item.Count);

   
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Count) 
                                   + (Delivery?.Amount ?? 0m);

        public Order(int id, IEnumerable<OrderItem> items)  // IEnumerable - итератор. Мы не знаем, что туда придёт, но эта штука позволит перебирать все элементы коллекции
        {
            Id = id;

            Items = new OrderItemCollection(items);
        }
    }
}
