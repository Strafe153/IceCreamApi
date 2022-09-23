using Application.Tests.Fixtures;
using Core.Entities;
using FluentAssertions;
using Moq;
using StackExchange.Redis;
using Xunit;

namespace Application.Tests;

public class IceCreamServiceTests : IClassFixture<IceCreamServiceFixture>
{
    private readonly IceCreamServiceFixture _fixture;

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
        result.Should().NotBeNull().And.NotBeEmpty().And.BeOfType<List<IceCream>>();
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
        result.Should().NotBeNull().And.BeOfType<IceCream>();
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
