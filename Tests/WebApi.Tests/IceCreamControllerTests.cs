using Core.Dtos;
using Core.Entities;
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
        public async Task GetAsync_AllItems_ReturnsActionResultOfIEnumerableOfIceCreamReadDto()
        {
            // Arrange
            _fixture.MockIceCreamService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(_fixture.IceCreams);

            _fixture.MockMapper
                .Setup(m => m.Map<IEnumerable<IceCreamReadDto>>(It.IsAny<IEnumerable<IceCream>>()))
                .Returns(_fixture.IceCreamReadDtos);

            // Act
            var result = await _fixture.MockIceCreamController.GetAsync();
            var readDtos = result.Result.As<OkObjectResult>().Value.As<IEnumerable<IceCreamReadDto>>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<IceCreamReadDto>>>();
            readDtos.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_ExistingIceCream_ReturnsActionResultOfIceCreamReadDto()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadDto);

            // Act
            var result = await _fixture.MockIceCreamController.GetAsync(_fixture.Id);
            var readDto = result.Result.As<OkObjectResult>().Value.As<IceCreamReadDto>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IceCreamReadDto>>();
            readDto.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateAsync_ValidDto_ReturnsActionResultOfIceCreamReadDto()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCream>(It.IsAny<IceCreamCreateUpdateDto>()))
                .Returns(_fixture.IceCream);

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadDto);

            // Act
            var result = await _fixture.MockIceCreamController.CreateAsync(_fixture.IceCreamCreateUpdateDto);
            var readDto = result.Result.As<CreatedAtActionResult>().Value.As<IceCreamReadDto>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IceCreamReadDto>>();
            readDto.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_ExistingUserValidDto_ReturnsNoContentResult()
        {
            // Arraange
            _fixture.MockIceCreamService
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.IceCream);

            _fixture.MockIceCreamService
                .Setup(s => s.UpdateAsync(It.IsAny<IceCream>()));

            _fixture.MockMapper
                .Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadDto);

            // Act
            var result = await _fixture.MockIceCreamController.UpdateAsync(_fixture.Id, _fixture.IceCreamCreateUpdateDto);

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
                .Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadDto);

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
                .Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(_fixture.IceCreamReadDto);

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
