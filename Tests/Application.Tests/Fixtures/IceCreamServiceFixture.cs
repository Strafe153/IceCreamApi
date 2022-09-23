using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Moq;
using StackExchange.Redis;

namespace Application.Tests.Fixtures;

public class IceCreamServiceFixture
{
    public IceCreamServiceFixture()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        MockIceCreamRepository = fixture.Freeze<Mock<IIceCreamRepository>>();
        MockIceCreamService = new IceCreamService(MockIceCreamRepository.Object);

        Id = Guid.NewGuid().ToString();
        IceCream = GetIceCream();
        IceCreams = GetIceCreams();
        RedisValue = GetRedisValue();
        HashEntries = GetHashEntries();
    }

    public Mock<IIceCreamRepository> MockIceCreamRepository { get; }
    public IIceCreamService MockIceCreamService { get; }

    public string Id { get; }
    public IceCream IceCream { get; }
    public List<IceCream> IceCreams { get; }
    public RedisValue RedisValue { get; }
    public HashEntry[] HashEntries { get; }

    private IceCream GetIceCream()
    {
        return new IceCream()
        {
            Id = Id,
            Flavour = "strawberry",
            Color = "red",
            Price = 3.25M,
            WeightInGrams = 60
        };
    }

    private List<IceCream> GetIceCreams()
    {
        return new List<IceCream>()
        {
            IceCream,
            IceCream
        };
    }

    private RedisValue GetRedisValue()
    {
        return new RedisValue("{}");
    }

    private HashEntry GetHashEntry()
    {
        return new HashEntry(RedisValue, RedisValue);
    }

    private HashEntry[] GetHashEntries()
    {
        return new HashEntry[]
        {
            GetHashEntry(),
            GetHashEntry()
        };
    }
}
