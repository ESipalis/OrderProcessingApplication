using Domain.Commands;
using Domain.Order.SpecificOrderData;
using MediatR;

namespace Domain.Order.Processor.SpecificOrderProcessor;

public class AgentCommissionOrderProcessor : ISpecificOrderProcessor
{
    private readonly IMediator _mediator;
    internal const decimal AgentCommissionValue = 7.11m;
    
    public AgentCommissionOrderProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool CheckOrderSuitabilityForProcessing(Order order)
    {
        return order.Data.TryPickT0(out PhysicalProductOrderData _, out _) || order.Data.TryPickT1(out BookOrderData _, out _);
    }

    public async Task ProcessOrder(Order order)
    {
        var addAgentCommissionCommand = new AddAgentCommissionCommand(order.AgentName, AgentCommissionValue);
        await _mediator.Send(addAgentCommissionCommand);
    }
}