using System;
using Client.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Services;

public static class ChatService
{
    public static async Task<Guid> CreateChatAsync(HttpClient httpClient, ContactModel contactModel)
    {
        var content = new StringContent(JsonConvert.SerializeObject(contactModel),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/chat/create", content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsAsync<Guid>();
    }
}