using Client.Models;
using Client.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services;

public class SaveEntityModelService
{
    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, Guid chatId,
        CancellationToken cancellationToken)
    {
        foreach (var message in messages) await SaveEntityAsync(message, cancellationToken);
    }

    public static async Task SaveEntityAsync(MessageModel message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);
    }

    public static async Task SaveEntityAsync(ContactModel contact, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{contact.Id}.json"), json,
            encoding, cancellationToken);
    }
}