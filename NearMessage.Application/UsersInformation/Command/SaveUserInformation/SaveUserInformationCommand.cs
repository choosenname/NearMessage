using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.UsersInformation;

namespace NearMessage.Application.UsersInformation.Command.SaveUserInformation;

public sealed record SaveUserInformationCommand(
    UserInformation UsersInformation,
    HttpContext HttpContext) : ICommand<Result>;