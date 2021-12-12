using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using IceCreamApi.Models;

namespace IceCreamApi.Repositories
{
    public class IceCreamRepository : IControllerRepository
    {
        private readonly IceCreamContext _context;

        public IceCreamRepository(IceCreamContext context)
        {
            _context = context;
        }

        public void AddIceCream(IceCream iceCream)
        {
            if (iceCream is null)
            {
                throw new ArgumentNullException(nameof(iceCream));
            }

            _context.IceCreams.Add(iceCream);
        }

        public async Task<IEnumerable<IceCream>> GetAllIceCreamsAsync()
        {
            return await _context.IceCreams.ToListAsync();
        }

        public async Task<IceCream> GetIceCreamByIdAsync(Guid id)
        {
            return await _context.IceCreams.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void DeleteIceCream(IceCream iceCream)
        {
            if (iceCream is null)
            {
                throw new ArgumentNullException(nameof(iceCream));
            }

            _context.IceCreams.Remove(iceCream);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateIceCream(IceCream iceCream)
        {
            if (iceCream is null)
            {
                throw new ArgumentNullException(nameof(iceCream));
            }

            _context.IceCreams.Update(iceCream);
        }
    }
}
