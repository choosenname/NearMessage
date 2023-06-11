using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed record class SaveMessageCommand(
    Message Message,
    HttpContext Context) : ICommand<Result>;