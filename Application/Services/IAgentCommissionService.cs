namespace Application.Services;

public interface IAgentCommissionService
{
    Task AddCommissionForAgent(string agentName, decimal amount);
}