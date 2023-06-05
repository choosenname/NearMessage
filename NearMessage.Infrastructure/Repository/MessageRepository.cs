using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NearMessage.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly string _filePath;

    public MessageRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<Result> SaveMessageAsync(User sender, Message message, CancellationToken cancellationToken)
    {
        string directoryPath = _filePath + $"{sender.Id}\\";
        Directory.CreateDirectory(directoryPath);

        string json = JsonSerializer.Serialize(message);

        await File.WriteAllTextAsync(_filePath + $"{sender.Id}\\{Guid.NewGuid()}.json", json, cancellationToken);

        return Result.Success();
    }
}
