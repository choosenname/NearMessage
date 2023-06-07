using NearMessage.Common.Abstractions.Messaging;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

//TODO: Return Result<MessagesResponse>
public sealed record class GetAllUsersQuery : IQuery<UsersResponse>;