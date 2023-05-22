using MediatR;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Entities;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class CreateUserAsyncCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly INearMessageDbContext _nearMessageDbContext;

    public CreateUserAsyncCommandHandler(INearMessageDbContext nearMessageDbContext)
    {
        _nearMessageDbContext = nearMessageDbContext;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.UserName,
            request.Password);

        await _nearMessageDbContext.Users.AddAsync(user);

        await _nearMessageDbContext.SaveChangesAsync(cancellationToken);
    }
}
