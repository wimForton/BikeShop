using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.BLL.Services.ShoppingBags
{
    public interface IShoppingBagsService
    {
        public Task CreateShoppingBagAsync(string userName);
        public Task<ShoppingBagModel> GetShoppingBagByIdAsync(int? id);
        public Task<ShoppingBagModel> GetShoppingBagWithDataByIdAsync(int? id);
        public Task<bool> DeleteConfirmedAsync(int? id);
        public Task<string> GetShoppingBagUserByIdAsync(int? id);
        public int AddItem(IdentityUser user, int myProductId, int myQuantity);
        public Task RemoveItem(int myItemId);
    }
}
