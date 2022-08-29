using Core.Entities;
using Core.ViewModels;
using FluentAssertions;
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
            _fixture.MockObjectModelValidator(_fixture.MockIceCreamController);
        }

        [Fact]
        public async Task GetAsync_AllItems_ReturnsActionResultOfIEnumerableOfIceCreamReadViewModel()
        {
            // Arrange
            _fixture.MockIceCreamService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(_fixture.IceCreams);

            _fixture.MockMapper
                .Setup(m => m.Map<IEnumerable<IceCreamReadViewModel>>(It.IsAny<IEnumerable<IceCream>>()))
                .Returns(_fixture.IceCreamReadViewModels);

            // Act
            var result = await _fixture.MockIceCreamController.GetAsync();
            var readViewModels = result.Result.As<OkObjectResult>().Value.As<IEnumerable<IceCreamReadViewModel>>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<IceCreamReadViewModel>>>();
            readViewModels.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_ExistingIceCream_ReturnsActionResultOfIceCreamReadViewModel()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadViewModel);

            // Act
            var result = await _fixture.MockIceCreamController.GetAsync(_fixture.Id);
            var readViewModel = result.Result.As<OkObjectResult>().Value.As<IceCreamReadViewModel>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IceCreamReadViewModel>>();
            readViewModel.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateAsync_ValidViewModel_ReturnsActionResultOfIceCreamReadViewModel()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCream>(It.IsAny<IceCreamCreateUpdateViewModel>()))
                .Returns(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadViewModel);

            // Act
            var result = await _fixture.MockIceCreamController.CreateAsync(_fixture.IceCreamCreateUpdateViewModel);
            var readViewModel = result.Result.As<CreatedAtActionResult>().Value.As<IceCreamReadViewModel>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IceCreamReadViewModel>>();
            readViewModel.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_ExistingUserValidViewModel_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadViewModel);

            // Act
            var result = await _fixture.MockIceCreamController.UpdateAsync(_fixture.Id, _fixture.IceCreamCreateUpdateViewModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateAsync_ExistingIceCreamValidPatchDocument_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadViewModel);

            // Act
            var result = await _fixture.MockIceCreamController.UpdateAsync(_fixture.Id, _fixture.JsonPatchDocument);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateAsync_ExistingIceCreamInvalidPatchDocument_ReturnsObjectResult()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadViewModel>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadViewModel);

            _fixture.MockModelError(_fixture.MockIceCreamController);

            // Act
            var result = await _fixture.MockIceCreamController.UpdateAsync(_fixture.Id, _fixture.JsonPatchDocument);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public async Task DeleteAsync_ExistingIceCream_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockIceCreamService
                .Setup(s => s.DeleteAsync(It.IsAny<IceCream>()));

            // Act
            var result = await _fixture.MockIceCreamController.DeleteAsync(_fixture.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
