using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Chats;

public interface IChatRepository
{
    Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken);

    Task<Result<Chat>> GetChatAsync(Guid sender, Guid receiver,
        CancellationToken cancellationToken);
}