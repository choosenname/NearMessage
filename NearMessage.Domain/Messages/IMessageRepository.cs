using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(User sender, Message message, CancellationToken cancellationToken);
}
