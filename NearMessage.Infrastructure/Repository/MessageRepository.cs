using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;
using System.Text.Json;

namespace NearMessage.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;

    public MessageRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken)
    {
        string directoryPath = _filePath + $"{chat.ChatId}\\";
        Directory.CreateDirectory(directoryPath);

        string json = JsonSerializer.Serialize(message);

        await File.WriteAllTextAsync(_filePath + $"{chat.ChatId}\\{message.Id}.json", json, cancellationToken);

        return Result.Success();
    }
}
