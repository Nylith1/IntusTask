using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.Handlers;
using IntusTask.Domain.Handlers.Requests;
using MediatR;
using NUnit.Framework;
using System.Text.Json;

namespace IntusTask.Tests.Handlers;

[TestFixture]
public class UpdateRectangleRequestHandlerTests
{
    private const string TestJsonFilePath = "test_rectangle.json";

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(TestJsonFilePath))
            File.Delete(TestJsonFilePath);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(TestJsonFilePath))
            File.Delete(TestJsonFilePath);
    }

    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Data, Is.EqualTo(Unit.Value));

        // Verify the file was written
        Assert.That(File.Exists(TestJsonFilePath), Is.True);

        var jsonContent = await File.ReadAllTextAsync(TestJsonFilePath);
        var deserializedRequest = JsonSerializer.Deserialize<UpdateRectangleRequest>(jsonContent);
        Assert.That(deserializedRequest?.Width, Is.EqualTo(request.Width));
        Assert.That(deserializedRequest?.Height, Is.EqualTo(request.Height));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenWidthIsZeroOrNegative()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Width and height must be positive values."));
        Assert.That(response.Data, Is.EqualTo(Unit.Value));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenHeightIsZeroOrNegative()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Width and height must be positive values."));
        Assert.That(response.Data, Is.EqualTo(Unit.Value));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenWidthExceedsHeight()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Width must not exceed height."));
        Assert.That(response.Data, Is.EqualTo(Unit.Value));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenWidthExceedsMaxWidth()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo($"Width can not exceed max rectangle width of: {Constants.MaxRectangleWidth}"));
        Assert.That(response.Data, Is.EqualTo(Unit.Value));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenHeightExceedsMaxHeight()
    {
        // Arrange
        var request = new UpdateRectangleRequest { Width = 10, Height = 15 };
        var handler = new UpdateRectangleRequestHandler();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo($"Height can not exceed max rectangle height of: {Constants.MaxRectangleHeight}"));
        Assert.That(response.Data, Is.EqualTo(Unit.Value));
    }
}
