using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class IceCreamService : IIceCreamService
    {
        private readonly IIceCreamRepository _repository;

        public IceCreamService(IIceCreamRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(IceCream iceCream)
        {
            _repository.Create(iceCream);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(IceCream iceCream)
        {
            _repository.Delete(iceCream);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<IceCream>> GetAllAsync()
        {
            var iceCreams = await _repository.GetAllAsync();
            return iceCreams;
        }

        public async Task<IceCream> GetByIdAsync(int id)
        {
            var iceCream = await _repository.GetByIdAsync(id);

            if (iceCream is null)
            {
                throw new NullReferenceException("Ice cream not found");
            }

            return iceCream;
        }

        public async Task UpdateAsync(IceCream iceCream)
        {
            _repository.Update(iceCream);
            await _repository.SaveChangesAsync();
        }
    }
}
