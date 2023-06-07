using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        await _nearMessageDbContext.Chats.AddAsync(chat);
        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(chat);
    }
}
