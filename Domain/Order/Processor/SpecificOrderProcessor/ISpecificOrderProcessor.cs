namespace Domain.Order.Processor.SpecificOrderProcessor;

public interface ISpecificOrderProcessor
{
    bool CheckOrderSuitabilityForProcessing(Order order);
    Task ProcessOrder(Order order);
}
