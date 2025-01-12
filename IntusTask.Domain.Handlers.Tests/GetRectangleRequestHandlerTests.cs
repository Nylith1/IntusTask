using IntusTask.Domain.Handlers.Handlers;
using IntusTask.Domain.Handlers.Requests;
using IntusTask.Domain.Handlers.Responses;
using NUnit.Framework;
using System.Text.Json;

namespace IntusTask.Tests.Handlers;

[TestFixture]
public class GetRectangleRequestHandlerTests
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
    public async Task Handle_ShouldReturnSuccess_WhenRectangleIsRead()
    {
        // Arrange
        var rectangle = new GetRectangleResponse { Width = 10, Height = 20 };
        var json = JsonSerializer.Serialize(rectangle);
        await File.WriteAllTextAsync(TestJsonFilePath, json);

        var handler = new GetRectangleRequestHandler();
        var request = new GetRectangleRequest();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Success, Is.True);
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data?.Width, Is.EqualTo(rectangle.Width));
        Assert.That(response.Data?.Height, Is.EqualTo(rectangle.Height));
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenFileDoesNotExist()
    {
        // Arrange
        var handler = new GetRectangleRequestHandler();
        var request = new GetRectangleRequest();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Rectangle could not be read."));
        Assert.That(response.Data, Is.Null);
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenFileHasInvalidJson()
    {
        // Arrange
        await File.WriteAllTextAsync(TestJsonFilePath, "Invalid JSON");

        var handler = new GetRectangleRequestHandler();
        var request = new GetRectangleRequest();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Rectangle could not be read."));
        Assert.That(response.Data, Is.Null);
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenFileIsEmpty()
    {
        // Arrange
        await File.WriteAllTextAsync(TestJsonFilePath, "");

        var handler = new GetRectangleRequestHandler();
        var request = new GetRectangleRequest();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Success, Is.False);
        Assert.That(response.ErrorMessage, Is.EqualTo("Rectangle could not be read."));
        Assert.That(response.Data, Is.Null);
    }
}
