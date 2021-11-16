using Domain.Commands;
using Domain.Order.SpecificOrderData;
using Domain.Order.SpecificOrderData.Extensions;
using Domain.PackingSlip;
using MediatR;

namespace Domain.Order.Processor.SpecificOrderProcessor;

public class BookOrderProcessor : ISpecificOrderProcessor
{
    // Should be injected instead of hard-coded consts.
    internal const string WarehouseName = "WAREHOUSE";
    internal const string WarehouseAddress = "Warehouse somewhere";
    internal const string RoyaltyName = "ROYALTY";
    internal const string RoyaltyAddress = "Royalty somewhere";
    
    public BookOrderProcessor(IMediator mediator)
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