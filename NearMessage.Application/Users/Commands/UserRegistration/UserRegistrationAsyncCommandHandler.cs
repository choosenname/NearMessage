using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;
using System.Windows.Input;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class UserRegistrationAsyncCommandHandler 
    : ICommandHandler<UserRegistrationCommand, Result<string>>
{
    private readonly INearMessageDbContext _nearMessageDbContext;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserRegistrationAsyncCommandHandler(INearMessageDbContext nearMessageDbContext
        , IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _nearMessageDbContext = nearMessageDbContext;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.Username,
            request.Password);

        var result = await _userRepository.CreateUserAsync(user, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<string>(result.Error);
        }

        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.Generate(user);

        return Result.Success(token);
    }
}
