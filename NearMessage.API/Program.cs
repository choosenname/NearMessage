using NearMessage.API.Hubs;
using NearMessage.Application;
using NearMessage.Infrastructure;
using NearMessage.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddSignalR();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();
app.AddRoutes();

app.MapHub<ChatHub>("/chat");

app.Run();
