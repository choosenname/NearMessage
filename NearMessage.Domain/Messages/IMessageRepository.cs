using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;

namespace NearMessage.Domain.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Chat chat, Message message, CancellationToken cancellationToken);
    Task<Result> SaveMediaAsync(Chat chat, Media media, CancellationToken cancellationToken);

    Task<Result<IEnumerable<Message>>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken);

    Task<Maybe<IEnumerable<Message>>> GetLastMessagesAsync(Guid chatId, DateTime lastMessageDate,
        CancellationToken cancellationToken);
}
