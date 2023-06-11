using System;
using Client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services;

public class SaveMessageService
{
    public static async Task SaveMessagesAsync(IEnumerable<MessageModel> messages, Guid chatId,
        CancellationToken cancellationToken)
    {
        foreach (var message in messages)
        {
            await SaveMessageAsync(message, chatId, cancellationToken);
        }

        return;
    }

    public static async Task SaveMessageAsync(MessageModel message, Guid chatId,
        CancellationToken cancellationToken)
    {
        string directoryPath = Path.Combine(
            Properties.Settings.Default.DataPath,
            chatId.ToString());

        Directory.CreateDirectory(directoryPath);

        string json = JsonConvert.SerializeObject(message);

        Encoding encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);

        return;
    }
}
