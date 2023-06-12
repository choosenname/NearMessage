using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.Chats;

public interface IChatRepository
{
    Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken);

    Task<Maybe<Chat>> GetChatByUsersIdAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken);

    Task<Maybe<Chat>> GetChatByIdAsync(Guid chatId, CancellationToken cancellationToken);
}