namespace Domain.Commands;

public record AddAgentCommissionCommand(string AgentName, decimal Amount) : ICommand;