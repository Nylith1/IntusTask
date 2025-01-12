using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.Requests;
using IntusTask.Domain.Handlers.Responses;
using MediatR;
using System.Text.Json;

namespace IntusTask.Domain.Handlers.Handlers;

public class GetRectangleRequestHandler : IRequestHandler<GetRectangleRequest, DataResponse<GetRectangleResponse>>
{
    public async Task<DataResponse<GetRectangleResponse>> Handle(GetRectangleRequest request, CancellationToken cancellationToken)
    {
        var rectangle = await GetRectangle(cancellationToken);
        if (rectangle is null)
            return new() { Success = false, ErrorMessage = "Rectangle could not be read." };
        else
            return new() { Success = true, Data = rectangle };
    }

    private static async Task<GetRectangleResponse?> GetRectangle(CancellationToken cancellationToken)
    {
        try
        {
            var json = await File.ReadAllTextAsync(Constants.JsonFilePath, cancellationToken);
            return JsonSerializer.Deserialize<GetRectangleResponse>(json);
        }
        catch
        {
            return null;
        }
    }
}