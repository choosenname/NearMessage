using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;

namespace NearMessage.Domain.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken);
}
