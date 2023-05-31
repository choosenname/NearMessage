using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.Messages;

public interface IMessageRepository
{
    Task<Result> SaveMessageAsync(Message message, CancellationToken cancellationToken);
}
