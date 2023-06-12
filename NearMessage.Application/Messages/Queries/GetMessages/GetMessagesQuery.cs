using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(
    Contact Sender) : IQuery<MessagesResponse>;