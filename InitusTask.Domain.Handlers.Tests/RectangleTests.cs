using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.ValueObjects;

namespace InitusTask.Domain.Handlers.Tests;

[TestFixture]
public class RectangleTests
{
    [Test]
    public void TryCreate_ShouldReturnSuccess_WhenValidWidthAndHeight()
    {
        // Arrange
        decimal width = 10;
        decimal height = 15;

        // Act
        var response = Rectangle.TryCreate(width, height);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data?.Width, Is.EqualTo(width));
        Assert.That(response.Data?.Height, Is.EqualTo(height));
    }

    [Test]
    public void TryCreate_ShouldReturnFailure_WhenWidthOrHeightIsZeroOrNegative()
    {
        // Arrange
        decimal invalidWidth = 0;
        decimal invalidHeight = -10;

        // Act
        var response = Rectangle.TryCreate(invalidWidth, invalidHeight);

        // Assert
        Assert.That(response.Success, Is.False);
    }

    [Test]
    public void TryCreate_ShouldReturnFailure_WhenWidthIsGreaterThanHeight()
    {
        // Arrange
        decimal width = 20;
        decimal height = 15;

        // Act
        var response = Rectangle.TryCreate(width, height);

        // Assert
        Assert.That(response.Success, Is.False);
    }

    [Test]
    public void TryCreate_ShouldReturnFailure_WhenWidthExceedsMaxWidth()
    {
        // Arrange
        decimal width = Constants.MaxRectangleWidth + 1;
        decimal height = 10;

        // Act
        var response = Rectangle.TryCreate(width, height);

        // Assert
        Assert.That(response.Success, Is.False);
    }

    [Test]
    public void TryCreate_ShouldReturnFailure_WhenHeightExceedsMaxHeight()
    {
        // Arrange
        decimal width = 10;
        decimal height = Constants.MaxRectangleHeight + 1;

        // Act
        var response = Rectangle.TryCreate(width, height);

        // Assert
        Assert.That(response.Success, Is.False);
    }
}