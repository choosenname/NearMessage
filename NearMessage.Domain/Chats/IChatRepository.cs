using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Chats;

public interface IChatRepository
{
    Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken);
}