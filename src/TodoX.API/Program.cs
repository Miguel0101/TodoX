using Microsoft.EntityFrameworkCore;
using TodoX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbCredentials"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();