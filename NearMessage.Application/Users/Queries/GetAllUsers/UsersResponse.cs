using NearMessage.Domain.Entities;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(IEnumerable<User> Users);