using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Moq;

namespace Application.Tests.Fixtures
{
    public class IceCreamServiceFixture
    {
        public IceCreamServiceFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            MockRepository = fixture.Freeze<Mock<IIceCreamRepository>>();
            MockService = new IceCreamService(MockRepository.Object);

            Id = 1;
            Flavour = "strawberry";
            Color = "red";
            Price = 3.25M;
            WeightInGrams = 60;
            IceCream = GetIceCream();
            IceCreams = GetIceCreams();
        }

        public Mock<IIceCreamRepository> MockRepository { get; }
        public IIceCreamService MockService { get; }

        public int Id { get; }
        public string Flavour { get; }
        public string Color { get; }
        public decimal Price { get; }
        public int WeightInGrams { get; }
        public IceCream IceCream { get; }
        public IEnumerable<IceCream> IceCreams { get; }

        public IceCream GetIceCream()
        {
            return new IceCream()
            {
                Id = Id,
                Flavour = Flavour,
                Color = Color,
                Price = Price,
                WeightInGrams = WeightInGrams
            };
        }

        public IEnumerable<IceCream> GetIceCreams()
        {
            return new List<IceCream>()
            {
                GetIceCream(),
                GetIceCream()
            };
        }
    }
}
