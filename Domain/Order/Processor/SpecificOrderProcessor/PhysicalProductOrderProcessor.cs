using Domain.Commands;
using Domain.Order.SpecificOrderData;
using Domain.Order.SpecificOrderData.Extensions;
using Domain.PackingSlip;
using MediatR;

namespace Domain.Order.Processor.SpecificOrderProcessor;

public class PhysicalProductOrderProcessor : ISpecificOrderProcessor
{
    // Should be injected instead of hard-coded consts.
    internal const string WarehouseName = "WAREHOUSE";
    internal const string WarehouseAddress = "Warehouse somewhere";
    
    private IMediator _mediator;

    public PhysicalProductOrderProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool CheckOrderSuitabilityForProcessing(Order order)
    {
        return order.Data.TryPickT0(out PhysicalProductOrderData _, out var _);
    }

    public async Task ProcessOrder(Order order)
    {
        PhysicalProductOrderData physicalProductOrderData = order.Data.AsT0;
        PackingSlip.PackingSlip packingSlip = physicalProductOrderData.GetPackingSlip(order);
        var generatePackingSlipCommand = new GeneratePackingSlipCommand(packingSlip, new PackingSlipRecipient(WarehouseName, WarehouseAddress));
        await _mediator.Send(generatePackingSlipCommand);
    }
}