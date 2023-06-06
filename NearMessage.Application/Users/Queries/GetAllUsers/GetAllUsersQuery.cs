using NearMessage.Common.Abstractions.Messaging;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed record class GetAllUsersQuery : IQuery<UsersResponse>;