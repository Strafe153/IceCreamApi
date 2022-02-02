using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using IceCreamApi.Dtos;
using IceCreamApi.Models;
using IceCreamApi.Controllers;
using IceCreamApi.Repositories;
using Moq;
using Xunit;
using AutoMapper;

namespace IceCreamApi.Tests
{
    public class IceCreamsControllerTests
    {
        private static readonly Mock<IControllerRepository> _repo = new Mock<IControllerRepository>();
        private static readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        [Fact]
        public async Task GetIceCreamsAsync_ExistingItems_ReturnsIceCreamReadDtos()
        {
            // Arrange
            _repo.Setup(r => r.GetAllIceCreamsAsync()).ReturnsAsync(
                new List<IceCream>()
                { 
                    new IceCream { Id =  Guid.Empty },
                    new IceCream { Id =  Guid.Empty },
                    new IceCream { Id =  Guid.Empty }
                });

            _mapper.Setup(m => m.Map<IEnumerable<IceCreamReadDto>>(
                It.IsAny<IEnumerable<IceCream>>())).Returns(
                    new List<IceCreamReadDto>()
                    {
                        new IceCreamReadDto(),
                        new IceCreamReadDto(),
                        new IceCreamReadDto()
                    });

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.GetIceCreamsAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<IceCreamReadDto>>(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetIceCreamByIdAsync_ExistingItem_ReturnsActionResultOfIceCreamReadDto()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.GetIceCreamByIdAsync(Guid.Empty);

            // Assert
            Assert.IsType<ActionResult<IceCreamReadDto>>(result);
        }

        [Fact]
        public async Task CreateIceCreamAsync_ValidDto_ReturnsActionRestulOfIceCream()
        {
            // Arrange
            _mapper.Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamReadDto() with { Id = Guid.Empty });

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.CreateIceCreamAsync(new IceCreamCreateUpdateDto());

            // Assert
            Assert.IsType<ActionResult<IceCream>>(result);
        }

        [Fact]
        public async Task UpdateIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.UpdateIceCreamAsync(Guid.Empty, new IceCreamCreateUpdateDto());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.UpdateIceCreamAsync(Guid.Empty, new IceCreamCreateUpdateDto());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PartialUpdateIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            _mapper.Setup(m => m.Map<IceCreamCreateUpdateDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamCreateUpdateDto());

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);
            MockObjectModelValidator(controller);

            // Act
            var result = await controller.PartialUpdateIceCreamAsync(Guid.Empty, 
                new JsonPatchDocument<IceCreamCreateUpdateDto>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PartialUpdateIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.PartialUpdateIceCreamAsync(Guid.Empty,
                new JsonPatchDocument<IceCreamCreateUpdateDto>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PartialUpdateIceCreamAsync_ExistingItemInvalidModel_ReturnsActionResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            _mapper.Setup(m => m.Map<IceCreamCreateUpdateDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamCreateUpdateDto());

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);
            MockObjectModelValidator(controller);

            // Act
            var result = await controller.PartialUpdateIceCreamAsync(Guid.Empty,
                new JsonPatchDocument<IceCreamCreateUpdateDto>());

            // Assert
            Assert.IsAssignableFrom<ActionResult>(result);
        }

        [Fact]
        public async Task DeleteIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.DeleteIceCreamAsync(Guid.Empty);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            _repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var controller = new IceCreamsController(_repo.Object, _mapper.Object);

            // Act
            var result = await controller.DeleteIceCreamAsync(Guid.Empty);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        private void MockObjectModelValidator(ControllerBase controller)
        {
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(), It.IsAny<string>(), It.IsAny<Object>()));

            controller.ObjectValidator = objectValidator.Object;
        }
    }
}
