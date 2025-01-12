using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.Requests;
using IntusTask.Domain.Handlers.ValueObjects;
using MediatR;
using System.Text.Json;

namespace IntusTask.Domain.Handlers.Handlers;

public class UpdateRectangleRequestHandler : IRequestHandler<UpdateRectangleRequest, DataResponse<Unit>>
{
    public async Task<DataResponse<Unit>> Handle(UpdateRectangleRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(10000, cancellationToken); // long-lasting calculation imitation.

        var createResult = Rectangle.TryCreate(request.Width, request.Height);
        if (!createResult.Success) return new() { Success = false, ErrorMessage = createResult.ErrorMessage };

        var json = JsonSerializer.Serialize(createResult.Data);
        await File.WriteAllTextAsync(Constants.JsonFilePath, json, cancellationToken);

        return new() { Success = true, Data = Unit.Value };
    }
}
