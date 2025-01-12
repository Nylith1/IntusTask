using IntusTask.Domain.Handlers.Common;

namespace IntusTask.Domain.Handlers.ValueObjects;

public class Rectangle
{
    private Rectangle(decimal width, decimal height)
    {
        Width = width;
        Height = height;
    }

    public decimal Width { get; private set; }
    public decimal Height { get; private set; }

    public static DataResponse<Rectangle> TryCreate(decimal width, decimal height)
    {
        var validationResult = Validate(width, height);
        if (validationResult.Count != 0)
            return new() { Success = false, ErrorMessage = string.Join("\n", validationResult) };

        return new() { Success = true, Data = new Rectangle(width, height) };
    }

    private static List<string> Validate(decimal width, decimal height)
    {
        var errors = new List<string>();

        if (width <= 0 || height <= 0)
            errors.Add("Width and height must be positive values.");

        if (width > height)
            errors.Add("Width must not exceed height.");

        if (width > Constants.MaxRectangleWidth)
            errors.Add($"Width can not exceed max rectangle width of: {Constants.MaxRectangleWidth}");

        if (height > Constants.MaxRectangleHeight)
            errors.Add($"Height can not exceed max rectangle height of: {Constants.MaxRectangleHeight}");

        return errors;
    }
}