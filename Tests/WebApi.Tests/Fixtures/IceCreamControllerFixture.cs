using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Fixtures
{
    public class IceCreamControllerFixture
    {
        public IceCreamControllerFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            MockIceCreamService = fixture.Freeze<Mock<IIceCreamService>>();
            MockMapper = fixture.Freeze<Mock<IMapper>>();

            MockIceCreamController = new IceCreamController(MockIceCreamService.Object, MockMapper.Object);

            Id = Guid.NewGuid().ToString();
            Flavour = "strawberry";
            Color = "red";
            Price = 3.25M;
            WeightInGrams = 60;
            JsonPatchDocument = GetJsonPatchDocument();
            IceCream = GetIceCream();
            IceCreamReadViewModel = GetIceCreamReadViewModel();
            IceCreamCreateUpdateViewModel = GetIceCreamCreateUpdateViewModel();
            IceCreams = GetIceCreams();
            IceCreamReadViewModels = GetIceCreamReadViewModels();
        }

        public Mock<IIceCreamService> MockIceCreamService { get; }
        public Mock<IMapper> MockMapper { get; }
        public IceCreamController MockIceCreamController { get; }

        public string Id { get; }
        public string Flavour { get; }
        public string Color { get; }
        public decimal Price { get; }
        public int WeightInGrams { get; }
        public JsonPatchDocument<IceCreamCreateUpdateViewModel> JsonPatchDocument { get; }
        public IceCream IceCream { get; }
        public IceCreamReadViewModel IceCreamReadViewModel { get; }
        public IceCreamCreateUpdateViewModel IceCreamCreateUpdateViewModel { get; }
        public IEnumerable<IceCream> IceCreams { get; }
        public IEnumerable<IceCreamReadViewModel> IceCreamReadViewModels { get; }

        public void MockObjectModelValidator(ControllerBase controller)
        {
            var objectValidator = new Mock<IObjectModelValidator>();

            objectValidator.Setup(o => o.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            controller.ObjectValidator = objectValidator.Object;
        }

        public void MockModelError(ControllerBase controller)
        {
            var context = new ControllerContext(
                new ActionContext(
                    new DefaultHttpContext()
                    {
                        TraceIdentifier = "trace"
                    },
                    new RouteData(),
                    new ControllerActionDescriptor()));

            context.ModelState.AddModelError("key", "error");
            controller.ControllerContext = context;
        }

        private JsonPatchDocument<IceCreamCreateUpdateViewModel> GetJsonPatchDocument()
        {
            return new JsonPatchDocument<IceCreamCreateUpdateViewModel>();
        }

        private IceCream GetIceCream()
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

        private IceCreamReadViewModel GetIceCreamReadViewModel()
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

        private IceCreamCreateUpdateViewModel GetIceCreamCreateUpdateViewModel()
        {
            return new IceCreamCreateUpdateViewModel()
            {
                Flavour = Flavour,
                Color = Color,
                Price = Price,
                WeightInGrams = WeightInGrams
            };
        }

        private IEnumerable<IceCream> GetIceCreams()
        {
            return new List<IceCream>()
            {
                GetIceCream(),
                GetIceCream()
            };
        }

        private IEnumerable<IceCreamReadViewModel> GetIceCreamReadViewModels()
        {
            return new List<IceCreamReadViewModel>()
            {
                GetIceCreamReadViewModel(),
                GetIceCreamReadViewModel()
            };
        }
    }
}
