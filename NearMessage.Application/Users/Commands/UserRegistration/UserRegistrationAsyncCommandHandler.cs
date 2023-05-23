using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;
using System.Windows.Input;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class UserRegistrationAsyncCommandHandler 
    : ICommandHandler<UserRegistrationCommand, Result>
{
    private readonly INearMessageDbContext _nearMessageDbContext;
    private readonly IUserRepository _userRepository;

    public UserRegistrationAsyncCommandHandler(INearMessageDbContext nearMessageDbContext
        , IUserRepository userRepository)
    {
        _nearMessageDbContext = nearMessageDbContext;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.UserName,
            request.Password);

        var result = await _userRepository.CreateUserAsync(user);

        if (result.IsFailure)
        {
            return result;
        }

        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
