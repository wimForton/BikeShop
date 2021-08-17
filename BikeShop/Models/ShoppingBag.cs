using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class ShoppingBag
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ShoppingItem> myShoppingItems { get; set; }
        public int myCustomerId { get; set; }
        public Customer myCustomer { get; set; }
    }
}
