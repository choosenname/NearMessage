using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed record class GetAllUsersQuery(
    HttpContext HttpContext) : IQuery<UsersResponse>;