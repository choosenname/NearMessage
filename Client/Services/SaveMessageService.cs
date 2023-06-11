using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Client.Properties;
using Newtonsoft.Json;

namespace Client.Services;

public class SaveMessageService
{
    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, Guid chatId,
        CancellationToken cancellationToken)
    {
        foreach (var message in messages) await SaveMessageAsync(message, chatId, cancellationToken);
    }

    public static async Task SaveMessageAsync(MessageModel message, Guid chatId,
        CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.DataPath,
            chatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);
    }
}