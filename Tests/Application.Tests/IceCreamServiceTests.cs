using Application.Tests.Fixtures;
using Core.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class IceCreamServiceTests : IClassFixture<IceCreamServiceFixture>, IDisposable
    {
        private readonly IceCreamServiceFixture _fixture;
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _fixture.MockRepository.Invocations.Clear();
            }

            _disposed = true;
        }

        public IceCreamServiceTests(IceCreamServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllAsync_AllItems_ReturnsIEnumerableOfIceCream()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(_fixture.IceCreams);

            // Act
            var result = await _fixture.MockService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<IceCream>>(result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingIceCream_ReturnsIceCream()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.IceCream);

            // Act
            var result = await _fixture.MockService.GetByIdAsync(_fixture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IceCream>(result);
        }

        [Fact]
        public async Task GetByIdAsync_NonexistingIceCream_ThrowsArgumentNullException()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((IceCream)null!);

            // Act
            var result = _fixture.MockService.GetByIdAsync(_fixture.Id);

            // Assert
            Assert.NotNull(result);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await result);
        }

        [Fact]
        public void CreateAsync_ValidData_ReturnsTask()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.Create(It.IsAny<IceCream>()));

            // Act
            var result = _fixture.MockService.CreateAsync(_fixture.IceCream);

            // Assert
            Assert.NotNull(result);
            _fixture.MockRepository.Verify(r => r.Create(It.IsAny<IceCream>()), Times.Once);
        }

        [Fact]
        public void UpdateAsync_ValidData_ReturnsTask()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.Update(It.IsAny<IceCream>()));

            // Act
            var result = _fixture.MockService.UpdateAsync(_fixture.IceCream);

            // Assert
            Assert.NotNull(result);
            _fixture.MockRepository.Verify(r => r.Update(It.IsAny<IceCream>()), Times.Once);
        }

        [Fact]
        public void DeleteAsync_ValidData_ReturnsTask()
        {
            // Arrange
            _fixture.MockRepository
                .Setup(r => r.Delete(It.IsAny<IceCream>()));

            // Act
            var result = _fixture.MockService.DeleteAsync(_fixture.IceCream);

            // Assert
            Assert.NotNull(result);
            _fixture.MockRepository.Verify(r => r.Delete(It.IsAny<IceCream>()), Times.Once);
        }
    }
}
