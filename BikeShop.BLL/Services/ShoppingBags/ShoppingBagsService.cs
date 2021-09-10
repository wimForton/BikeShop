using BikeShop.DAL.Data.Repositories.ShoppingBag;
using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.BLL.Services.ShoppingBags
{
    public class ShoppingBagsService : IShoppingBagsService
    {
        private readonly IShoppingBagRepository _ShoppingBagsRepository;
        public ShoppingBagsService(IShoppingBagRepository ShoppingBagsRepository)
        {
            _ShoppingBagsRepository = ShoppingBagsRepository;
        }


        public async Task CreateShoppingBagAsync(string userName)
        {
            await _ShoppingBagsRepository.CreateShoppingBagAsync(userName);
        }
        public int AddItem(IdentityUser user, int myProductId, int myQuantity)
        {
            return _ShoppingBagsRepository.AddItem(user, myProductId, myQuantity);
        }

        public async Task<bool> DeleteConfirmedAsync(int? id)
        {
            return await _ShoppingBagsRepository.DeleteConfirmedAsync(id);
        }

        public async Task<ShoppingBagModel> GetShoppingBagByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagByIdAsync(id);
        }

        public async Task<string> GetShoppingBagUserByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagUserByIdAsync(id);
        }

        public async Task<ShoppingBagModel> GetShoppingBagWithDataByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagWithDataByIdAsync(id);
        }

        public async Task RemoveItem(int myItemId)
        {
            await _ShoppingBagsRepository.RemoveItem(myItemId);
        }
    }
}
