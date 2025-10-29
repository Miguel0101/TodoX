using Microsoft.EntityFrameworkCore;
using TodoX.Application;
using TodoX.Application.Common.Interfaces;
using TodoX.Infrastructure;
using TodoX.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Variables
IConfigurationSection jwtSection = builder.Configuration.GetSection("JwtConfiguration");
JwtConfiguration jwtConfig = jwtSection.Get<JwtConfiguration>()!;

builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

// Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(jwtConfig, options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbCredentials"));
});

builder.Services.Configure<JwtConfiguration>(jwtSection);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"2FA Token: {TokenService.Generate2FAToken()}");

app.Run();