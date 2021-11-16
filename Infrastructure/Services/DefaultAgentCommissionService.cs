using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class DefaultAgentCommissionService : IAgentCommissionService
{
    private readonly ILogger<DefaultAgentCommissionService> _logger;

    public DefaultAgentCommissionService(ILogger<DefaultAgentCommissionService> logger)
    {
        _logger = logger;
    }

    public Task AddCommissionForAgent(string agentName, decimal amount)
    {
        _logger.LogTrace("Adding {CommissionAmount} commission to agent '{AgentName}'", amount, agentName);
        return Task.CompletedTask;
    }
}