using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.Text.Json;

namespace Application.Services;

public class IceCreamService : IIceCreamService
{
    private readonly IIceCreamRepository _repository;

    public IceCreamService(IIceCreamRepository repository)
    {
        _repository = repository;
    }

    public async Task UpdateAsync(IceCream iceCream)
    {
        await _repository.UpdateAsync(iceCream);
    }

    public async Task DeleteAsync(IceCream iceCream)
    {
        bool result = await _repository.DeleteAsync(iceCream);

        if (!result)
        {
            throw new OperationFailedException($"Failed to delete the ice-cream with the id {iceCream.Id}");
        }
    }

    public async Task<IEnumerable<IceCream>> GetAllAsync()
    {
        var hashEntries = await _repository.GetAllAsync();
        var iceCreams = Array.ConvertAll(hashEntries, e => JsonSerializer.Deserialize<IceCream>(e.Value)!).ToList();

        return iceCreams;
    }

    public async Task<IceCream> GetByIdAsync(string id)
    {
        var redisValue = await _repository.GetByIdAsync(id);

        if (!redisValue.HasValue)
        {
            throw new NullReferenceException($"Ice-cream with id '{id}' not found");
        }

        var iceCream = JsonSerializer.Deserialize<IceCream>(redisValue)!;

        return iceCream;
    }

    public async Task CreateAsync(IceCream iceCream)
    {
        await _repository.CreateAsync(iceCream);
    }
}
