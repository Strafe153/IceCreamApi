using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IIceCreamRepository
    {
        Task<IEnumerable<IceCream>> GetAllAsync();
        Task<IceCream?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        void Create(IceCream iceCream);
        void Update(IceCream iceCream);
        void Delete(IceCream iceCream);
    }
}
