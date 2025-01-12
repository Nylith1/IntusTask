namespace IntusTask.Domain.Handlers.Responses;

public record GetRectangleResponse
{
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
}
