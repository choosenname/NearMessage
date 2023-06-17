using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using Newtonsoft.Json;
using System.Text;

namespace NearMessage.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;

    public MessageRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<Result<IEnumerable<Media>>> GetMessagesAsync(Guid chatId,
        CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        var fileNames = Directory.GetFiles(directoryPath);

        List<Media> messages = new();

        foreach (var fileName in fileNames)
        {
            var json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<Media>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return Result.Success<IEnumerable<Media>>(messages);
    }

    public async Task<Maybe<IEnumerable<Media>>> GetLastMessagesAsync(Guid chatId,
        DateTime lastMessageDate, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        var lastModified = Directory.GetLastWriteTime(directoryPath);

        if (lastModified <= lastMessageDate) return Maybe<IEnumerable<Media>>.None;

        var files = Directory.GetFiles(directoryPath);
        var messages = new List<Media>();

        foreach (var file in files)
        {
            var creationTime = File.GetCreationTime(file);

            if (creationTime > lastMessageDate)
            {
                var json = await File.ReadAllTextAsync(file, cancellationToken);
                var message = JsonConvert.DeserializeObject<Media>(json);
                if (message == null) continue;

                messages.Add(message);
            }
        }

        return Maybe<IEnumerable<Media>>.From(messages);
    }

    public async Task<Result> SaveMessageAsync(Media message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, message.ReceiverChatId.ToString());
        Directory.CreateDirectory(directoryPath);

        var json = JsonConvert.SerializeObject(message);

        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }
}