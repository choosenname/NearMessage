using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NearMessage.API.OptionsSetup;
using NearMessage.Application;
using NearMessage.Infrastructure.Authentication;
using NearMessage.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddCarter();


// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapCarter();

app.Run();
