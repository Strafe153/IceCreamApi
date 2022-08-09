using Core.Entities;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Tests.Fixtures;
using Xunit;

namespace WebApi.Tests
{
    public class IceCreamControllerTests : IClassFixture<IceCreamControllerFixture>
    {
        private readonly IceCreamControllerFixture _fixture;

        public IceCreamControllerTests(IceCreamControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAsync_AllItems_ReturnsOkObjectResult()
        {
            // Arrange
            _fixture.MockService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(_fixture.IceCreams);

            _fixture.MockMapper
                .Setup(m => m.Map<IEnumerable<IceCreamReadViewModel>>(It.IsAny<IEnumerable<IceCream>>()))
                .Returns(_fixture.ReadModels);

            // Act
            var result = await _fixture.MockController.GetAsync();
            var readModels = (result.Result as OkObjectResult)!.Value as IEnumerable<IceCreamReadViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IEnumerable<IceCreamReadViewModel>>>(result);
            Assert.NotEmpty(readModels!);
        }

        [Fact]
        public async Task GetAsync_ExistingUser_ReturnsOkObjectResult()
        {
            // Arraange
            _fixture.MockService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.ReadModel);

            // Act
            var result = await _fixture.MockController.GetAsync(_fixture.Id);
            var readModel = (result.Result as OkObjectResult)!.Value as IceCreamReadViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IceCreamReadViewModel>>(result);
            Assert.NotNull(readModel);
        }

        [Fact]
        public async Task CreateAsync_ValidData_ReturnsCreatedAtActionResult()
        {
            // Arraange
            _fixture.MockService
                .Setup(s => s.CreateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCream>(It.IsAny<IceCreamCreateUpdateViewModel>()))
                .Returns(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.ReadModel);

            // Act
            var result = await _fixture.MockController.CreateAsync(_fixture.CreateUpdateModel);
            var readModel = (result.Result as CreatedAtActionResult)!.Value as IceCreamReadViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IceCreamReadViewModel>>(result);
            Assert.NotNull(readModel);
        }

        [Fact]
        public async Task UpdateAsync_ExistingUser_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.ReadModel);

            // Act
            var result = await _fixture.MockController.UpdateAsync(_fixture.Id, _fixture.CreateUpdateModel);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_PatchRequestExistingUser_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.ReadModel);

            // Act
            var result = await _fixture.MockController.UpdateAsync(_fixture.Id, _fixture.PatchDocument);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingUser_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockService
                .Setup(s => s.DeleteAsync(It.IsAny<IceCream>()));

            // Act
            var result = await _fixture.MockController.DeleteAsync(_fixture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}