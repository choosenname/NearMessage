using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Services;

public static class GroupService
{
    public static async Task CreateGroupAsync(string name, HttpClient httpClient)
    {
        var content = new StringContent(JsonConvert.SerializeObject(name),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/group/create", content, CancellationToken.None);

        response.EnsureSuccessStatusCode();
    }

    public static async Task<Guid> CreateUserGroupAsync(Guid groupId, HttpClient httpClient)
    {
        var content = new StringContent(JsonConvert.SerializeObject(groupId),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/group/join", content, CancellationToken.None);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsAsync<Guid>(CancellationToken.None);
    }
}