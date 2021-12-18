using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string CellPhone { get; set; }

        public string DeliveryUniqueCode { get; set; }

        public string DeliveryDescription { get; set; }

        public decimal DeliveryPrice { get; set; }

        public Dictionary<string, string> DeliveryParameters { get; set; }

        public string PaymentServiceName { get; set; }

        public string PaymentDescription { get; set; }

        public Dictionary<string, string> PaymentParameters { get; set; }

        public IList<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
