using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public sealed record MessagesResponse(Result<IEnumerable<Media>> Messages);