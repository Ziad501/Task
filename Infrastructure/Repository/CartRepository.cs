using Application.Interfaces;
using Domain.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();

        }

        public async Task<bool> DeleteCartASync(Guid id)
        {
            return await _database.KeyDeleteAsync(id.ToString());
        }

        public async Task<Cart?> GetCartAsync(Guid id)
        {
            var data =  await _database.StringGetAsync(id.ToString());
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart?>(data!);
        }

        public async Task<Cart?> SetCartAsync(Cart cart)
        {
            var created = await _database.StringSetAsync(cart.Id.ToString(), JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if(!created) { return null; }
            return await GetCartAsync(cart.Id);
        }
    }
}
