using Core.Entities;

namespace Core.Interfaces.Services;

public interface IIceCreamService
{
    Task<IEnumerable<IceCream>> GetAllAsync();
    Task<IceCream> GetByIdAsync(string id);
    Task CreateAsync(IceCream iceCream);
    Task UpdateAsync(IceCream iceCream);
    Task DeleteAsync(IceCream iceCream);
}
