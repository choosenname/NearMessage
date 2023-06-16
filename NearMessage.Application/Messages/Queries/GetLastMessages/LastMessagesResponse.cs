using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Queries.GetLastMessages;

public sealed record class LastMessagesResponse(Result<IEnumerable<Message>> Messages);