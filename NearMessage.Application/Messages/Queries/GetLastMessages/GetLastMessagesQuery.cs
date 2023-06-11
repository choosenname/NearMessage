using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;

namespace NearMessage.Application.Messages.Queries.GetLastMessages;

public sealed record class GetLastMessagesQuery(
    DateTime LastResponseTime,
    HttpContext HttpContext) : IQuery<LastMessagesResponse>;