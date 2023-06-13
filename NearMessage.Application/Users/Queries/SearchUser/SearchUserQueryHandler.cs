using NearMessage.Application.Abstraction;
using NearMessage.Application.Users.Queries.GetAllUsers;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.SearchUser;

public sealed class SearchUserQueryHandler : IQueryHandler<SearchUserQuery, SearchedUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public SearchUserQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<SearchedUserResponse> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeSenderId.HasNoValue)
            return new SearchedUserResponse(
                Result.Failure<IEnumerable<Contact>>(new Error("Used don't recognized")));

        var users = await _userRepository.GetUsersByUsernameAsync(request.Username, cancellationToken);


        var contacts = users.Select(u =>
            new Contact(
                u.Id,
                u.Username,
                u.SentChats?.Find(
                    c => c.ReceiverId == maybeSenderId.Value)?
                    .ChatId));

        return new SearchedUserResponse(Result.Success(contacts));
    }
}