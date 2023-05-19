using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Repository_Interfaces;

namespace NearMessage.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IApplicationDbContext applicationDbContext)
    {
        _userRepository = userRepository;
        _applicationDbContext = applicationDbContext;
    }

    public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
