using Client.Models;
using Client.Properties;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services;

public class SaveEntityModelService
{
    public static event EventHandler? MessagesSaved;
    private static readonly object _messageLock = new();
    private static readonly object _contactLock = new();

    public static void SaveEntity(MessageModel message)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");

        lock (_messageLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }
    }

    public static void SaveEntity(ContactModel contact)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        lock (_contactLock)
        {
            File.WriteAllText(filePath, json, encoding);
        }
    }

    public static async Task SaveEntityAsync(MessageModel message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            message.ReceiverChatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{message.Id}.json");


        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }

    public static async Task SaveEntityAsync(ContactModel contact, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserDataPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }

    public static async Task SaveEntityAsync(UserInformationModel contact, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserInformationPath);

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(contact);

        var encoding = Encoding.UTF8;

        var filePath = Path.Combine(directoryPath, $"{contact.Id}.json");

        await File.WriteAllTextAsync(filePath, json, encoding, cancellationToken);
    }

    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, CancellationToken cancellationToken)
    {
        foreach (var message in messages) await SaveEntityAsync(message, cancellationToken);

        OnMessagesSaved(EventArgs.Empty);
    }

    public static void SaveMessages(IEnumerable<MessageModel> messages)
    {
        foreach (var message in messages) SaveEntity(message);

        OnMessagesSaved(EventArgs.Empty);
    }


    protected static void OnMessagesSaved(EventArgs e)
    {
        MessagesSaved?.Invoke(null, e);
    }
}