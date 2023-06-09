using Client.Models;
using Client.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Commands;

public class SendMediaCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly ContactModel _contactReceiver;
    private readonly ChatViewModel _chatViewModel;

    public SendMediaCommand(HttpClient httpClient, ContactModel contactReceiver,
        ChatViewModel chatViewModel)
    {
        _httpClient = httpClient;
        _contactReceiver = contactReceiver;
        _chatViewModel = chatViewModel;
    }

    public override async void Execute(object? parameter)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Все файлы (*.*)|*.*";

        bool? result = openFileDialog.ShowDialog();

        if (result == true)
        {
            var selectedFilePath = openFileDialog.FileName;
            var fileData = await File.ReadAllBytesAsync(selectedFilePath);
            var fileName = Path.GetFileName(selectedFilePath);

            var message = new MediaModel(
                Guid.NewGuid(),
                _chatViewModel.MessageText,
                _contactReceiver.Id,
                fileData,
                fileName);

            var content = new StringContent(JsonConvert.SerializeObject(message),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/message/sendfile", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
