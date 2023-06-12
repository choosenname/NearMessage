using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Chats.Commands.GetChat;

public sealed record GetChatCommand(
    Contact Contact,
    HttpContext HttpContext) : ICommand<Result<Chat>>;