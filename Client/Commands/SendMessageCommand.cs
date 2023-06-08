using Client.Models;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Client.Commands;

public class SendMessageCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;
    private readonly ContactModel _contactReceiver;
    private readonly HttpClient _httpClient;

    public SendMessageCommand(ChatViewModel chatViewModel, ContactModel contactReceiver,
        HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _contactReceiver = contactReceiver;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var message = new MessageModel(
            Guid.NewGuid(),
            _chatViewModel.MessageText,
            _contactReceiver.Id);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/send", content);

        response.EnsureSuccessStatusCode();
    }
}
