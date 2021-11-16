using Domain.PackingSlip;

namespace Domain.Order.SpecificOrderData.Extensions;

public static class PhysicalProductOrderDataExtensions
{
    public static PackingSlip.PackingSlip GetPackingSlip(this PhysicalProductOrderData physicalProductOrderData, Order order)
    {
        var packingSlipItems = new List<PackingSlipItem> { new(physicalProductOrderData.Sku, physicalProductOrderData.Quantity) };
        return new PackingSlip.PackingSlip(order.Id, packingSlipItems);
    }
}