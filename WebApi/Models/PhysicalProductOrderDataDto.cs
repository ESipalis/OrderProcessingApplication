using Domain.Order.SpecificOrderData;

namespace WebApi.Models;

public record PhysicalProductOrderDataDto(string Sku, int Quantity);

public static class PhysicalProductOrderDataDtoExtensions
{
    public static PhysicalProductOrderData ToDomain(this PhysicalProductOrderDataDto dto)
    {
        return new PhysicalProductOrderData(dto.Sku, dto.Quantity);
    }
}