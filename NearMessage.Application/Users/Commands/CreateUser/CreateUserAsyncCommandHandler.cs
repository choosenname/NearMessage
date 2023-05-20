using NearMessage.Application.Abstraction;
using NearMessage.Application.Abstraction.Messaging;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Repository_Interfaces;
using NearMessage.Domain.Shared;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class CreateUserAsyncCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserAsyncCommandHandler(
        IUserRepository userRepository,
        IApplicationDbContext applicationDbContext)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            Guid.NewGuid(),
            request.UserName,
            request.Password);

        await _userRepository.CreateUserAsync(user);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
