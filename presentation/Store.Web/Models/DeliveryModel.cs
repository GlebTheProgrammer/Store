using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class DeliveryModel
    {
        public int OrderId { get; set; }

        public Dictionary<string,string> Methods { get; set; }
    }
}
