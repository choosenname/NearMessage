using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMedia;

public sealed record SaveMediaCommand(
    Media Media,
    HttpContext Context) : ICommand<Result>;