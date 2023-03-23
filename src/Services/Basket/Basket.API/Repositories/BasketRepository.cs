using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<ShoppingCart> Get(string userName)
        {
            var cart = await _cache.GetStringAsync(userName);

            return string.IsNullOrEmpty(cart) ? null : JsonConvert.DeserializeObject<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> Update(ShoppingCart cart)
        {
            await _cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await Get(cart.UserName);
        }

        public async Task Delete(string userName)
        {
            await _cache.RemoveAsync(userName);
        }
    }
}
