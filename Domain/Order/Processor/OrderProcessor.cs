using Domain.Order.Processor.SpecificOrderProcessor;

namespace Domain.Order.Processor;

public class OrderProcessor
{
    private readonly List<ISpecificOrderProcessor> _specificOrderProcessors;

    public OrderProcessor(IEnumerable<ISpecificOrderProcessor> specificOrderProcessors)
    {
        _specificOrderProcessors = specificOrderProcessors.ToList();
    }

    public async Task ProcessOrder(Order order)
    {
        IEnumerable<ISpecificOrderProcessor> processorsToRun = _specificOrderProcessors.Where(specificOrderProcessor => specificOrderProcessor.CheckOrderSuitabilityForProcessing(order));
        foreach (ISpecificOrderProcessor specificOrderProcessor in processorsToRun)
        {
            await specificOrderProcessor.ProcessOrder(order);
        }
    }
}