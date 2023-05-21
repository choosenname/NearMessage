using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Application.Abstraction.Messaging;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Repository_Interfaces;
using NearMessage.Domain.Shared;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class CreateUserAsyncCommandHandler 
    : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly INearMessageDbContext _nearMessageDbContext;

    public CreateUserAsyncCommandHandler(
        IUserRepository userRepository,
        INearMessageDbContext nearMessageDbContext)
    {
        _userRepository = userRepository;
        _nearMessageDbContext = nearMessageDbContext;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.UserName,
            request.Password);

        await _userRepository.CreateUserAsync(user);

        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(id);
    }
}
