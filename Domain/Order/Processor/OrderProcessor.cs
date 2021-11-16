using Domain.Order.Processor.SpecificOrderProcessor;
using Microsoft.Extensions.Logging;

namespace Domain.Order.Processor;

public class OrderProcessor
{
    private readonly List<ISpecificOrderProcessor> _specificOrderProcessors;
    private readonly ILogger<OrderProcessor> _logger;

    public OrderProcessor(IEnumerable<ISpecificOrderProcessor> specificOrderProcessors, ILogger<OrderProcessor> logger)
    {
        _logger = logger;
        _specificOrderProcessors = specificOrderProcessors.ToList();
    }

    public async Task ProcessOrder(Order order)
    {
        IEnumerable<ISpecificOrderProcessor> processorsToRun = _specificOrderProcessors.Where(specificOrderProcessor => specificOrderProcessor.CheckOrderSuitabilityForProcessing(order));
        foreach (ISpecificOrderProcessor specificOrderProcessor in processorsToRun)
        {
            await specificOrderProcessor.ProcessOrder(order);
            _logger.LogTrace("Processed order with processor: {Processor}", specificOrderProcessor.GetType()); // Reflection usage...
        }
    }
}