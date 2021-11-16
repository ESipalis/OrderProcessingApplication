namespace Domain.Order.Processor.SpecificOrderProcessor;

public interface ISpecificOrderProcessor
{
    bool CheckOrderSuitabilityForProcessing(Order order);
    void ProcessOrder(Order order);
}
