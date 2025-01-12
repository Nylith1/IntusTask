using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.Responses;
using MediatR;

namespace IntusTask.Domain.Handlers.Requests;

public record GetRectangleRequest : IRequest<DataResponse<GetRectangleResponse>>
{
}
