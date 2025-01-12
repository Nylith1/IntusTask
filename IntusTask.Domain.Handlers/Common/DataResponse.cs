namespace IntusTask.Domain.Handlers.Common;

public record DataResponse<T>
{
    public required bool Success { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;
    public T? Data { get; init; }
}