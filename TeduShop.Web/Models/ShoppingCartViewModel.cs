using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ShoppingCartViewModel
    {
        public int ID { get; set; }
        public ProductViewModel product { get; set; }
        public int Quantity { get; set; }
    }
}