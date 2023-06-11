using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.SearchUser;

public sealed class SearchUserQueryHandler : IQueryHandler<SearchUserQuery, SearchedUserResponse>
{
    private readonly IUserRepository _userRepository;

    public SearchUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<SearchedUserResponse> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersByUsernameAsync(request.Username, cancellationToken);

        var contacts = users.Select(u =>
            new Contact(
                u.Id,
                u.Username,
                null));

        return new SearchedUserResponse(Result.Success(contacts));
    }
}