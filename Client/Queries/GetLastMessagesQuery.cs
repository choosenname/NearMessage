﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Client.Commands;
using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;

namespace Client.Queries;

public class GetLastMessagesQuery : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;

    public GetLastMessagesQuery(HttpClient httpClient, UserStore userStore)
    {
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public override async void Execute(object? parameter)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync($"/messages/getlast" 
                                                  + $"?LastResponseTime ={_userStore.LastResponseTime:yyyy-MM-ddTHH:mm:ss}");

        _userStore.LastResponseTime = DateTime.Now;

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var contacts = await response.Content
            .ReadAsAsync<IDictionary<Guid, IEnumerable<MessageModel>>>();

        foreach (var contact in contacts)
        {
            await SaveMessageService.SaveMessagesAsync(contact.Value, contact.Key, CancellationToken.None);
        }
    }
}