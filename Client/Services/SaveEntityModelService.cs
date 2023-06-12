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
    public static async Task SaveMessagesAsync(IEnumerable<EntityModel> entities, Guid chatId,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities) await SaveMessageAsync(entity, chatId, cancellationToken);
    }

    public static async Task SaveMessageAsync(EntityModel entity, Guid chatId,
        CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.MessagesDataPath,
            chatId.ToString());

        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(entity);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{entity.Id}.json"), json,
            encoding, cancellationToken);
    }
}