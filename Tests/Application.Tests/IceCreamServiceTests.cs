using Application.Tests.Fixtures;
using Core.Entities;
using FluentAssertions;
using Moq;
using StackExchange.Redis;
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
                _fixture.MockIceCreamRepository.Invocations.Clear();
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
            _fixture.MockIceCreamRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(_fixture.HashEntries);

            // Act
            var result = await _fixture.MockIceCreamService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().BeOfType<List<IceCream>>();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingIceCream_ReturnsIceCream()
        {
            // Arrange
            _fixture.MockIceCreamRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.RedisValue);

            // Act
            var result = await _fixture.MockIceCreamService.GetByIdAsync(_fixture.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<IceCream>();
        }

        [Fact]
        public async Task GetByIdAsync_NonexistingIceCream_ThrowsNullReferenceException()
        {
            // Arrange
            _fixture.MockIceCreamRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .Throws<NullReferenceException>();

            // Act
            var result = async () => await _fixture.MockIceCreamService.GetByIdAsync(_fixture.Id);

            // Assert
            result.Should().NotBeNull();
            await result.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public void CreateAsync_ValidIceCream_ReturnsTask()
        {
            // Act
            var result = async () => await _fixture.MockIceCreamService.CreateAsync(_fixture.IceCream);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAsync_ValidIceCream_ReturnsTask()
        {
            // Act
            var result = async () => await _fixture.MockIceCreamService.UpdateAsync(_fixture.IceCream);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void DeleteAsync_ValidIceCream_ReturnsTask()
        {
            // Act
            var result = async () => await _fixture.MockIceCreamService.DeleteAsync(_fixture.IceCream);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
