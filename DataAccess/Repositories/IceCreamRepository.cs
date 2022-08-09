using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class IceCreamRepository : IIceCreamRepository
    {
        private readonly IceCreamContext _context;

        public IceCreamRepository(IceCreamContext context)
        {
            _context = context;
        }

        public void Create(IceCream iceCream)
        {
            _context.IceCreams.Add(iceCream);
        }

        public void Delete(IceCream iceCream)
        {
            _context.IceCreams.Remove(iceCream);
        }

        public async Task<IEnumerable<IceCream>> GetAllAsync()
        {
            var iceCreams = await _context.IceCreams.ToListAsync();
            return iceCreams;
        }

        public async Task<IceCream?> GetByIdAsync(int id)
        {
            var iceCream = await _context.IceCreams.SingleOrDefaultAsync(i => i.Id == id);
            return iceCream;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(IceCream iceCream)
        {
            _context.IceCreams.Update(iceCream);
        }
    }
}
