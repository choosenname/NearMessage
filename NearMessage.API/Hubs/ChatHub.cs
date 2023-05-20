using Microsoft.AspNetCore.SignalR;

namespace NearMessage.API.Hubs;

public class ChatHub : Hub
{
    public Task Send(string message)
    {
        return Clients.All.SendAsync("Recive", message);
    }
}
