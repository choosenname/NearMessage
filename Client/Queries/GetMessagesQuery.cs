using Client.Commands;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Client.Queries;

internal class GetMessagesQuery : CommandBase
{
    private readonly ChatViewModel _chatViewModel;
    private readonly HttpClient _httpClient;

    public GetMessagesQuery(ChatViewModel chatViewModel, HttpClient httpClient)
    {
        _chatViewModel = chatViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(_chatViewModel.CurrentContact),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/get", content);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        var messages = await response.Content
            .ReadAsAsync<ObservableCollection<MessageModel>>();

        _chatViewModel.Messages = new ObservableCollection<MessageModel>(
            messages.OrderBy(i => i.SendTime));

        await SaveMessageService.SaveMessagesAsync(messages, _chatViewModel.CurrentContact, default);
    }
}
