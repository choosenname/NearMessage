using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Chats.Commands.CreateChat;

public sealed record CreateChatCommand(
    Contact Contact,
    HttpContext HttpContext) : ICommand<Result<Chat>>;