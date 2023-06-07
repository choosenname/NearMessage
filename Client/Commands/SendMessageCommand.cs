using Client.Models;
using Client.Stores;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Client.Commands;

public class SendMessageCommand : CommandBase
{
    private readonly ContactModel _receiver;
    
    private readonly string _message;

    private readonly HttpClient _httpClient;

    public SendMessageCommand(ContactModel receiver, 
        string message, HttpClient httpClient)
    {
        _receiver = receiver;
        _message = message;
        _httpClient = httpClient;
    }

    public async override void Execute(object? parameter)
    {
        var message = new MessageModel(
            Guid.NewGuid(),
            _message,
            _receiver);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/receiver", content);

        response.EnsureSuccessStatusCode();
    }
}
