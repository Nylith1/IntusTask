using IntusTask.Domain.Handlers.Common;
using MediatR;

namespace IntusTask.Domain.Handlers.Requests;

public record UpdateRectangleRequest : IRequest<DataResponse<Unit>>
{
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
}
