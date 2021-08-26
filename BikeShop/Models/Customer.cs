using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Models
{
    public class Customer
    {
        public IdentityUser IdentityUser { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public List<ShoppingBag> myShoppingBags { get; set; }
    }
}
