using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class OrderItemModel  // Data Transfer Object DTO
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}
