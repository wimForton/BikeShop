using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Productid { get; set; }
        public Product myProduct { get; set; }
        public int myShoppingBagId { get; set; }
        public ShoppingBag myShoppingBag { get; set; }

    }
}
