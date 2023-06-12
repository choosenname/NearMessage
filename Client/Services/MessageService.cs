using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Properties;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Services;

public static class MessageService
{
    public static async Task<ObservableCollection<MessageModel>?> LoadLocalMessagesAsync(
        ContactModel contact, CancellationToken cancellationToken)
    {
        if (contact.ChatId == null)
            return null;

        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            contact.ChatId.ToString()!);

        var fileNames = Directory.GetFiles(directoryPath);

        ObservableCollection<MessageModel> messages = new();

        foreach (var fileName in fileNames)
        {
            var json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<MessageModel>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return messages;
    }

    public static async Task<ObservableCollection<MessageModel>?> SaveMessagesAsync(
        ContactModel contact,HttpClient httpClient, CancellationToken cancellationToken)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(contact),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/message/get", content, cancellationToken);

        if (!response.IsSuccessStatusCode) return null;
        var messages = await response.Content
            .ReadAsAsync<ObservableCollection<MessageModel>>(cancellationToken);

        if (!contact.ChatId.HasValue) return null;

        await SaveEntityModelService.SaveMessagesAsync(
        messages,
            contact.ChatId.Value,
            CancellationToken.None);

        return messages;
    }
}