using IntusTask.Domain.Handlers.Common;
using IntusTask.Domain.Handlers.Responses;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddMediatR(cfg =>
{
    var assemblies = new[]
    {
        Assembly.GetExecutingAssembly(),
        Assembly.Load("IntusTask.Domain.Handlers"),
    };
    cfg.RegisterServicesFromAssemblies(assemblies);
});

SeedJsonFileIfNeeded();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


static void SeedJsonFileIfNeeded()
{
    if (!File.Exists(Constants.JsonFilePath))
    {
        var defaultRectangle = new GetRectangleResponse { Width = Constants.DefaultRectangleWidth, Height = Constants.DefaultRectangleHeight };
        var defaultJson = JsonSerializer.Serialize(defaultRectangle);
        File.WriteAllTextAsync(Constants.JsonFilePath, defaultJson);
    }
}