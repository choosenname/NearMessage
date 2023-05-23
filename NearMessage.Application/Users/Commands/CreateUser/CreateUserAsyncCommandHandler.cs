using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class CreateUserAsyncCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly INearMessageDbContext _nearMessageDbContext;
    private readonly IUserRepository _userRepository;

    public CreateUserAsyncCommandHandler(INearMessageDbContext nearMessageDbContext
        , IUserRepository userRepository)
    {
        _nearMessageDbContext = nearMessageDbContext;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.UserName,
            request.Password);

        await _userRepository.CreateUserAsync(user);

        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);
    }
}
