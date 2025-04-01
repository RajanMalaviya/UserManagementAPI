using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI support for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the middleware pipeline
app.UseMiddleware<LoggingMiddleware>();  // Log requests and responses
app.UseMiddleware<ErrorHandlingMiddleware>();  // Handle errors
app.UseMiddleware<AuthenticationMiddleware>();  // Authentication middleware

app.UseAuthorization();
app.MapControllers();

app.Run();
