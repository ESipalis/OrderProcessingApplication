using Application.Services;
using Domain.Commands;
using MediatR;

namespace Application.CommandHandlers;

public class AddAgentCommissionCommandHandler : ICommandHandler<AddAgentCommissionCommand>
{
    private readonly IAgentCommissionService _agentCommissionService;


    public AddAgentCommissionCommandHandler(IAgentCommissionService agentCommissionService)
    {
        _agentCommissionService = agentCommissionService;
    }

    public async Task<Unit> Handle(AddAgentCommissionCommand command, CancellationToken cancellationToken)
    {
        await _agentCommissionService.AddCommissionForAgent(command.AgentName, command.Amount);
        return Unit.Value;
    }
}