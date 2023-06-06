using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NearMessage.API.OptionsSetup;
using NearMessage.Application;
using NearMessage.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddCarter();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapCarter();

app.Run();
