using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using IceCreamApi.Models;

namespace IceCreamApi.Repositories
{
    public interface IControllerRepository
    {
        Task<IEnumerable<IceCream>> GetAllIceCreamsAsync();
        Task<IceCream> GetIceCreamByIdAsync(Guid id);
        Task SaveChangesAsync();
        void AddIceCream(IceCream iceCream);
        void UpdateIceCream(IceCream iceCream);
        void RemoveIceCream(IceCream iceCream);
    }
}
