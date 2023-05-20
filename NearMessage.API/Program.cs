using Infrastructure;
using NearMessage.API.Hubs;
using NearMessage.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSignalR();
builder.Services.AddControllers();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chat");

app.MapControllers();

app.Run();
