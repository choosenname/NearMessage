﻿using System.Net.Http;
using System.Threading;
using Client.Models;
using Client.Services;
using Client.ViewModels;

namespace Client.Commands;

public class LoadMessagesCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public LoadMessagesCommand(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        _chatViewModel.Messages =
            await MessageService.LoadLocalMessagesAsync(_chatViewModel.CurrentContact, CancellationToken.None)
            ?? await MessageService.SaveMessagesAsync(
                _chatViewModel.CurrentContact, _httpClient, CancellationToken.None);
    }
}