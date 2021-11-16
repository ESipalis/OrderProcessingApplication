using Domain.Order.Processor.SpecificOrderProcessor;

namespace Domain.Order.Processor;

public class OrderProcessor
{
    public OrderProcessor(IEnumerable<ISpecificOrderProcessor> specificOrderProcessors)
    {
        throw new InvalidOperationException();
    }

    public bool ProcessOrder(Order order)
    {
        throw new InvalidOperationException();
    }
}