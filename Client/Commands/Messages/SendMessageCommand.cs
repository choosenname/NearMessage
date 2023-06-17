using Client.Models;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Client.Services;
using Client.Stores;

namespace Client.Commands.Messages;

public class SendMessageCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;
    private readonly ContactModel _contactReceiver;
    private readonly HttpClient _httpClient;

    public SendMessageCommand(ChatViewModel chatViewModel, ContactModel contactReceiver, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _contactReceiver = contactReceiver;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        _contactReceiver.ChatId ??=
            await ChatService.CreateChatAsync(_httpClient, _contactReceiver, CancellationToken.None);

        if (_contactReceiver.Id == Guid.Empty)
        {
            _contactReceiver.Id =
                await GroupService.CreateUserGroupAsync(_chatViewModel.CurrentContact.ChatId!.Value, _httpClient);
        }

        var message = new MediaModel(
            Guid.NewGuid(),
            _chatViewModel.MessageText,
            Guid.Empty,
            _contactReceiver.ChatId.Value);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/send", content);

        response.EnsureSuccessStatusCode();
    }
}