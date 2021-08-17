using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public List<ShoppingBag> myShoppingBags { get; set; }
    }
}
