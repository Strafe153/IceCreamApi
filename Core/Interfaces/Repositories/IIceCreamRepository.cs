using Core.Entities;
using StackExchange.Redis;

namespace Core.Interfaces.Repositories;

public interface IIceCreamRepository
{
    Task<HashEntry[]> GetAllAsync();
    Task<RedisValue> GetByIdAsync(string id);
    Task CreateAsync(IceCream iceCream);
    Task UpdateAsync(IceCream iceCream);
    Task<bool> DeleteAsync(IceCream iceCream);
}
