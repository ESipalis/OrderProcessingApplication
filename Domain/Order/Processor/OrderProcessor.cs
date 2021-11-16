using Domain.Order.Processor.SpecificOrderProcessor;

namespace Domain.Order.Processor;

public class OrderProcessor
{
    public OrderProcessor(IEnumerable<ISpecificOrderProcessor> specificOrderProcessors)
    {
        throw new InvalidOperationException();
    }

    public void ProcessOrder(Order order)
    {
        throw new InvalidOperationException();
    }
}