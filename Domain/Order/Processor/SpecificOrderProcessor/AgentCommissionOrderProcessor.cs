using MediatR;

namespace Domain.Order.Processor.SpecificOrderProcessor;

public class AgentCommissionOrderProcessor : ISpecificOrderProcessor
{
    internal const decimal AgentCommissionValue = 7.11m;
    
    public AgentCommissionOrderProcessor(IMediator mediator)
    {
        throw new InvalidOperationException();
    }

    public bool CheckOrderSuitabilityForProcessing(Order order)
    {
        throw new InvalidOperationException();
    }

    public async Task ProcessOrder(Order order)
    {
        throw new InvalidOperationException();
    }
}