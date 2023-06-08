using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<UsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var maybeUser = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeUser.HasNoValue)
        {
            return new UsersResponse(Result.Failure<IEnumerable<Contact>?>(new("Used don't recognized")));
        }

        var user = await _userRepository.GetByIdAsync(maybeUser.Value, cancellationToken);

        if(user.HasNoValue)
        {
            return new UsersResponse(Result.Failure<IEnumerable<Contact>?>(new("Used doesn't exist")));
        }

        var contacts = user.Value.ReceivedChats
            ?.Select(c => new Contact(
                c.Sender.Id,
                c.Sender.Username,
                c.ChatId))
            .ToList();

        return new UsersResponse(Result.Success<IEnumerable<Contact>?>(contacts));
    }
}