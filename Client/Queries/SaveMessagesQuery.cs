using Client.Commands;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Queries;

public class SaveMessagesQuery : CommandBase
{
    private readonly ContactModel _currentContact;
    private readonly HttpClient _httpClient;

    public SaveMessagesQuery(ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(_currentContact),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/get", content);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        var messages = await response.Content
            .ReadAsAsync<ObservableCollection<MessageModel>>();

        await SaveMessageService.SaveMessagesAsync(messages, _currentContact, default);
    }
}
