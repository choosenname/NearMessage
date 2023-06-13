using System;
using System.Net.Http;
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
        var messages =
            await MessageService.LoadLocalMessagesAsync(_chatViewModel.CurrentContact, CancellationToken.None);

        if (messages == null || messages.Count == 0)
        {
            messages = await MessageService.SaveMessagesAsync(
                _chatViewModel.CurrentContact, _httpClient, CancellationToken.None);
        } 

        _chatViewModel.Messages = messages;
    }
}