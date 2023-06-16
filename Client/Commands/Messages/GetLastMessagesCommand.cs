using Client.Models;
using Client.Services;
using Client.Stores;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Commands.Messages;

public class GetLastMessagesCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;

    public GetLastMessagesCommand(HttpClient httpClient, UserStore userStore)
    {
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public override async void Execute(object? parameter)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync($"/message/getlast"
                                                  + $"?LastResponseTime={_userStore.LastResponseTime:yyyy-MM-ddTHH:mm:ss}");

        _userStore.LastResponseTime = DateTime.Now;

        if (!response.IsSuccessStatusCode) return;

        var contacts = await response.Content
            .ReadAsAsync<IEnumerable<MessageModel>>();

        SaveEntityModelService.SaveMessages(contacts);
    }
}