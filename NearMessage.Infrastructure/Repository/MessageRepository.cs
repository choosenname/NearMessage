using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;
using Newtonsoft.Json;
using System.Text;
using NearMessage.Common.Primitives.Maybe;
using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;

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

    public async Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid chatId,
        CancellationToken cancellationToken)
    {
        string directoryPath = Path.Combine(_filePath, chatId.ToString());
        string[] fileNames = Directory.GetFiles(directoryPath);

        List<Message> messages = new();

        foreach (string fileName in fileNames)
        {
            string json = await File.ReadAllTextAsync(fileName, cancellationToken);
            var message = JsonConvert.DeserializeObject<Message>(json);
            if (message == null) continue;

            messages.Add(message);
        }

        return Result.Success<IEnumerable<Message>>(messages);
    }

    public async Task<Maybe<IEnumerable<Message>>> GetLastMessagesAsync(Guid chatId,
        DateTime lastMessageDate, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chatId.ToString());
        DateTime lastModified = Directory.GetLastWriteTime(directoryPath);

        if (lastModified <= lastMessageDate)
        {
            return Maybe<IEnumerable<Message>>.None;
        }

        string[] files = Directory.GetFiles(directoryPath);
        var messages = new List<Message>();

        foreach (string file in files)
        {
            DateTime creationTime = File.GetCreationTime(file);

            if (creationTime > lastMessageDate)
            {
                string json = await File.ReadAllTextAsync(file, cancellationToken);
                var message = JsonConvert.DeserializeObject<Message>(json);
                if (message == null) continue;

                messages.Add(message);
            }
        }

        return Maybe<IEnumerable<Message>>.From(messages);
    }

    public async Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_filePath, chat.ChatId.ToString());
        Directory.CreateDirectory(directoryPath);

        string json = JsonConvert.SerializeObject(message);

        Encoding encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{message.Id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> SaveMediaAsync(Chat chat, Media media, CancellationToken cancellationToken)
    {
        string directoryPath = Path.Combine(_filePath, chat.ChatId.ToString());
        Directory.CreateDirectory(directoryPath);

        string json = JsonConvert.SerializeObject(media);

        Encoding encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(directoryPath, $"{media.Id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }
}