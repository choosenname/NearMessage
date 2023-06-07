using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(
    Contact Sender,
    HttpContext Context) : IQuery<MessagesResponse>;
