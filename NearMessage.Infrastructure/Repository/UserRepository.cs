using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly INearMessageDbContext _context;

    public UserRepository(INearMessageDbContext nearMessageDbContext)
    {
        _context = nearMessageDbContext;
    }

    public async Task<Result<Contact>> ConvertToContactAsync( User user, Guid sender,
        CancellationToken cancellationToken)
    {
        var chat = await _context.Chats
            .FirstOrDefaultAsync(c => 
                    c.SenderId == sender && c.ReceiverId == user.Id,
                cancellationToken: cancellationToken);

        if (chat == null)
        {
            return Result.Failure<Contact>(new Error($"Chat for contact {user.Id} doesn't exist"));
        }

        return Result.Success(new Contact(
            user.Id,
            user.Username,
            chat.ChatId));

        throw new NotImplementedException();
    }

    public async Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user);
    }

    public async Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
    {
        var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id, cancellationToken);

        return user == null ? Maybe<User>.None : Maybe<User>.From(user);
    }

    public async Task<Maybe<User>> GetByUsernameAsync(string userName, CancellationToken cancellationToken) =>
        await _context.Users.SingleOrDefaultAsync(i => i.Username == userName, cancellationToken);
}