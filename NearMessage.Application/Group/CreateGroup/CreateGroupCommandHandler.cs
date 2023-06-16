using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Groups;

namespace NearMessage.Application.Group.CreateGroup;

public sealed record CreateGroupCommandHandler
: ICommandHandler<CreateGroupCommand, Result<Domain.Groups.Group>>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<Domain.Groups.Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Domain.Groups.Group(
            Guid.NewGuid(),
            request.Name);

        var groupResult = await _groupRepository.CreateGroupAsync(group, cancellationToken);

        return groupResult;
    }
}