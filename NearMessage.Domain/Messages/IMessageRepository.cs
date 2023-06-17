using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Media message, CancellationToken cancellationToken);

    Task<Result<IEnumerable<Media>>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken);

    Task<Maybe<IEnumerable<Media>>> GetLastMessagesAsync(Guid chatId, DateTime lastMessageDate,
        CancellationToken cancellationToken);
}