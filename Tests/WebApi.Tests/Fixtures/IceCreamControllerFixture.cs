using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Fixtures;

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
        IceCreamReadDto = GetIceCreamReadDto();
        IceCreamCreateUpdateDto = GetIceCreamCreateUpdateDto();
        IceCreams = GetIceCreams();
        IceCreamReadDtos = GetIceCreamReadDtos();
    }

    public Mock<IIceCreamService> MockIceCreamService { get; }
    public Mock<IMapper> MockMapper { get; }
    public IceCreamController MockIceCreamController { get; }

    public string Id { get; }
    public string Flavour { get; }
    public string Color { get; }
    public decimal Price { get; }
    public int WeightInGrams { get; }
    public JsonPatchDocument<IceCreamCreateUpdateDto> JsonPatchDocument { get; }
    public IceCream IceCream { get; }
    public IceCreamReadDto IceCreamReadDto { get; }
    public IceCreamCreateUpdateDto IceCreamCreateUpdateDto { get; }
    public IEnumerable<IceCream> IceCreams { get; }
    public IEnumerable<IceCreamReadDto> IceCreamReadDtos { get; }

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

    private JsonPatchDocument<IceCreamCreateUpdateDto> GetJsonPatchDocument()
    {
        return new JsonPatchDocument<IceCreamCreateUpdateDto>();
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

    private IceCreamReadDto GetIceCreamReadDto()
    {
        return new IceCreamReadDto()
        {
            Id = Id,
            Flavour = Flavour,
            Color = Color,
            Price = Price,
            WeightInGrams = WeightInGrams
        };
    }

    private IceCreamCreateUpdateDto GetIceCreamCreateUpdateDto()
    {
        return new IceCreamCreateUpdateDto()
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

    private IEnumerable<IceCreamReadDto> GetIceCreamReadDtos()
    {
        return new List<IceCreamReadDto>()
        {
            GetIceCreamReadDto(),
            GetIceCreamReadDto()
        };
    }
}
