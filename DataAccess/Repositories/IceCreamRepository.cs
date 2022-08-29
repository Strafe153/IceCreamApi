using Core.Entities;
using Core.Interfaces.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class IceCreamRepository : IIceCreamRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;
        private readonly string _hashKey = "IceCreamHash";

        public IceCreamRepository(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _database = _redisConnection.GetDatabase();
        }

        public async Task UpdateAsync(IceCream iceCream)
        {
            await CreateAsync(iceCream);
        }

        public async Task<bool> DeleteAsync(IceCream iceCream)
        {
            bool result = await _database.HashDeleteAsync(_hashKey, iceCream.Id);
            return result;
        }

        public async Task<HashEntry[]> GetAllAsync()
        {
            var hashEntry = await _database.HashGetAllAsync(_hashKey);
            return hashEntry;
        }

        public async Task<RedisValue> GetByIdAsync(string id)
        {
            var hashEntry = await _database.HashGetAsync(_hashKey, id);
            return hashEntry;
        }

        public async Task CreateAsync(IceCream iceCream)
        {
            var serializedIceCream = JsonSerializer.Serialize(iceCream);

            await _database.HashSetAsync(_hashKey, new HashEntry[]
            {
                new HashEntry(iceCream.Id, serializedIceCream)
            });
        }
    }
}
