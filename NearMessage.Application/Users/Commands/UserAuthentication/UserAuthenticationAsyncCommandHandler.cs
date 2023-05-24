using MediatR;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Commands.UserAuthentication;

public sealed class UserAuthenticationAsyncCommandHandler
    : ICommandHandler<UserAuthenticationCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public UserAuthenticationAsyncCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetUserByNameAsync(request.UserName, cancellationToken);

        if(maybeUser.HasNoValue)
        {
            Result.Failure(new("The user with the specified user name was not found."));
        }

        User user = maybeUser.Value;

        if (!user.VerifyPassword(request.Password))
        {
            return Result.Failure(new("Password wasn't match"));
        }

        return Result.Success();
    }
}
