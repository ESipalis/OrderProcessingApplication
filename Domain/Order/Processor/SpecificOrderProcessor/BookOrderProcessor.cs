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
    
    private IMediator _mediator;

    public BookOrderProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public bool CheckOrderSuitabilityForProcessing(Order order)
    {
        return order.Data.TryPickT1(out BookOrderData _, out _);
    }

    public async Task ProcessOrder(Order order)
    {
        PhysicalProductOrderData physicalProductOrderData = order.Data.AsT0;
        PackingSlip.PackingSlip packingSlip = physicalProductOrderData.GetPackingSlip(order);
        var warehousePackingSlipCommand = new GeneratePackingSlipCommand(packingSlip, new PackingSlipRecipient(WarehouseName, WarehouseAddress));
        await _mediator.Send(warehousePackingSlipCommand);
        var royaltyPackingSlipCommand = new GeneratePackingSlipCommand(packingSlip, new PackingSlipRecipient(RoyaltyName, RoyaltyAddress));
        await _mediator.Send(royaltyPackingSlipCommand);
    }
}