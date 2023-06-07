using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;
using Newtonsoft.Json;
using System.Text;

namespace NearMessage.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;
    private readonly IChatRepository _chatRepository;

    public MessageRepository(string filePath, IChatRepository chatRepository)
    {
        _filePath = filePath;
        _chatRepository = chatRepository;
    }

    public async Task<Result<List<Message>>> GetMessagesAsync(Guid receiver, Guid sender,
        CancellationToken cancellationToken)
    {
        var chatResult = await _chatRepository.GetChatAsync(sender, receiver, cancellationToken);

        if (chatResult.IsFailure)
        {
            return Result.Failure<List<Message>>(chatResult.Error);
        }

        string directoryPath = Path.Combine(_filePath, chatResult.Value.ChatId.ToString());
        string[] fileNames = Directory.GetFiles(directoryPath);

        List<Message> messages = new();

        foreach (string fileName in fileNames)
        {
            string json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<Message>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return Result.Success(messages);
    }

    public async Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken)
    {
        string directoryPath = Path.Combine(_filePath, chat.ChatId.ToString());
        Directory.CreateDirectory(directoryPath);

        string json = JsonConvert.SerializeObject(message);

        Encoding encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }
}
