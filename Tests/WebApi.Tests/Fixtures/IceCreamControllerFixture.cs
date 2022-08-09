using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Fixtures
{
    public class IceCreamControllerFixture
    {
        public IceCreamControllerFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            MockService = fixture.Freeze<Mock<IIceCreamService>>();
            MockMapper = fixture.Freeze<Mock<IMapper>>();

            MockController = new IceCreamController(MockService.Object, MockMapper.Object);
            MockObjectModelValidator(MockController);

            Id = 1;
            Flavour = "strawberry";
            Color = "red";
            Price = 3.25M;
            WeightInGrams = 60;
            PatchDocument = GetPatchDocument();
            IceCream = GetIceCream();
            ReadModel = GetReadModel();
            CreateUpdateModel = GetCreateUpdateModel();
            IceCreams = GetIceCreams();
            ReadModels = GetReadModels();
        }

        public Mock<IIceCreamService> MockService { get; }
        public Mock<IMapper> MockMapper { get; }
        public IceCreamController MockController { get; }

        public int Id { get; }
        public string Flavour { get; }
        public string Color { get; }
        public decimal Price { get; }
        public int WeightInGrams { get; }
        public JsonPatchDocument<IceCreamCreateUpdateViewModel> PatchDocument { get; }
        public IceCream IceCream { get; }
        public IceCreamReadViewModel ReadModel { get; }
        public IceCreamCreateUpdateViewModel CreateUpdateModel { get; }
        public IEnumerable<IceCream> IceCreams { get; }
        public IEnumerable<IceCreamReadViewModel> ReadModels { get; }

        public JsonPatchDocument<IceCreamCreateUpdateViewModel> GetPatchDocument()
        {
            return new JsonPatchDocument<IceCreamCreateUpdateViewModel>();
        }

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

        public IceCreamReadViewModel GetReadModel()
        {
            return new IceCreamReadViewModel()
            {
                Id = Id,
                Flavour = Flavour,
                Color = Color,
                Price = Price,
                WeightInGrams = WeightInGrams
            };
        }

        public IceCreamCreateUpdateViewModel GetCreateUpdateModel()
        {
            return new IceCreamCreateUpdateViewModel()
            {
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

        public IEnumerable<IceCreamReadViewModel> GetReadModels()
        {
            return new List<IceCreamReadViewModel>()
            {
                GetReadModel(),
                GetReadModel()
            };
        }

        private void MockObjectModelValidator(ControllerBase controller)
        {
            var objectValidator = new Mock<IObjectModelValidator>();

            objectValidator.Setup(o => o.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            controller.ObjectValidator = objectValidator.Object;
        }
    }
}
