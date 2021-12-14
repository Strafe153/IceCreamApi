using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
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
        [Fact]
        public void GetIceCreamsAsync_ExistingItems_ReturnsIceCreamReadDtos()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetAllIceCreamsAsync()).ReturnsAsync(
                new List<IceCream>()
                { 
                    new IceCream { Id =  Guid.Empty },
                    new IceCream { Id =  Guid.Empty },
                    new IceCream { Id =  Guid.Empty }
                });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IceCreamReadDto>>(
                It.IsAny<IEnumerable<IceCream>>())).Returns(
                    new List<IceCreamReadDto>()
                    {
                        new IceCreamReadDto(),
                        new IceCreamReadDto(),
                        new IceCreamReadDto()
                    });

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.GetIceCreamsAsync().Result;

            // Assert
            Assert.IsAssignableFrom<IEnumerable<IceCreamReadDto>>(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetIceCreamByIdAsync_ExistingItem_ReturnsActionResultOfIceCreamReadDto()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.GetIceCreamByIdAsync(Guid.Empty).Result;

            // Assert
            Assert.IsType<ActionResult<IceCreamReadDto>>(result);
        }

        [Fact]
        public void CreateIceCreamAsync_ValidDto_ReturnsActionRestulOfIceCream()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IceCreamReadDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamReadDto() with { Id = Guid.Empty });

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.CreateIceCreamAsync(new IceCreamCreateUpdateDto()).Result;

            // Assert
            Assert.IsType<ActionResult<IceCream>>(result);
        }

        [Fact]
        public void UpdateIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.UpdateIceCreamAsync(Guid.Empty, new IceCreamCreateUpdateDto()).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.UpdateIceCreamAsync(Guid.Empty, new IceCreamCreateUpdateDto()).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialUpdateIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IceCreamCreateUpdateDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamCreateUpdateDto());

            var controller = new IceCreamsController(repo.Object, mapper.Object);
            MockObjectModelValidator(controller);

            // Act
            var result = controller.PartialUpdateIceCreamAsync(Guid.Empty, 
                new JsonPatchDocument<IceCreamCreateUpdateDto>()).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PartialUpdateIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.PartialUpdateIceCreamAsync(Guid.Empty,
                new JsonPatchDocument<IceCreamCreateUpdateDto>()).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialUpdateIceCreamAsync_ExistingItemInvalidModel_ReturnsActionResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IceCreamCreateUpdateDto>(It.IsAny<IceCream>()))
                .Returns(new IceCreamCreateUpdateDto());

            var controller = new IceCreamsController(repo.Object, mapper.Object);
            MockObjectModelValidator(controller);

            // Act
            var result = controller.PartialUpdateIceCreamAsync(Guid.Empty,
                new JsonPatchDocument<IceCreamCreateUpdateDto>()).Result;

            // Assert
            Assert.IsAssignableFrom<ActionResult>(result);
        }

        [Fact]
        public void DeleteIceCreamAsync_ExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new IceCream());

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.DeleteIceCreamAsync(Guid.Empty).Result;

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Fact]
        public void DeleteIceCreamAsync_NonexistingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var repo = new Mock<IControllerRepository>();
            repo.Setup(r => r.GetIceCreamByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IceCream)null);

            var mapper = new Mock<IMapper>();

            var controller = new IceCreamsController(repo.Object, mapper.Object);

            // Act
            var result = controller.DeleteIceCreamAsync(Guid.Empty).Result;

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
