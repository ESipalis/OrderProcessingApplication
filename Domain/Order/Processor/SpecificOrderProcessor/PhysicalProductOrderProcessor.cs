using MediatR;

namespace Domain.Order.Processor.SpecificOrderProcessor;

public class PhysicalProductOrderProcessor : ISpecificOrderProcessor
{

    public PhysicalProductOrderProcessor(IMediator mediator)
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