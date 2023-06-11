using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;

namespace NearMessage.Infrastructure.Repository;

internal class ChatRepository : IChatRepository
{
    private readonly INearMessageDbContext _nearMessageDbContext;

    public ChatRepository(INearMessageDbContext nearMessageDbContext)
    {
        _nearMessageDbContext = nearMessageDbContext;
    }

    public async Task<Result<Chat>> CreateChatAsync(Guid user1, Guid user2,
        CancellationToken cancellationToken)
    {
        var chat = new Chat(
            Guid.NewGuid(),
            user1,
            user2);

        await _nearMessageDbContext.Chats.AddAsync(chat, cancellationToken);
        await _nearMessageDbContext.Chats.AddAsync(chat.InversedChat, cancellationToken);
        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(chat);
    }

    public async Task<Maybe<Chat>> GetChatByIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var chat = await _nearMessageDbContext.Chats.SingleOrDefaultAsync(c =>
            c.ChatId == chatId, cancellationToken);

        return chat == null ? Maybe<Chat>.None : Maybe<Chat>.From(chat);
    }

    public async Task<Maybe<Chat>> GetChatByUsersAsync(Guid user1, Guid user2, CancellationToken cancellationToken)
    {
        var chat = await _nearMessageDbContext.Chats
            .FirstOrDefaultAsync(c =>
                    c.Sender.Id == user1 && c.Receiver.Id == user2,
                cancellationToken);

        if (chat == null) return Maybe<Chat>.None;

        return Maybe<Chat>.From(chat);
    }
}