using Domain.Order;
using Domain.Order.SpecificOrderData;
using OneOf;

namespace WebApi.Models;

public record ProcessOrderDto(
    long Id,
    DateTimeOffset ConfirmationDate,
    string AgentName,
    // Add validation to ensure only one of them is set.
    PhysicalProductOrderDataDto? PhysicalProductOrderData,
    BookOrderDataDto? BookOrderData,
    MembershipActivationOrderDataDto? MembershipActivationOrderData,
    MembershipUpgradeOrderDataDto? MembershipUpgradeOrderData
);

public static class ProcessOrderDtoExtensions
{
    public static Order ToDomain(this ProcessOrderDto dto)
    {
        OneOf<PhysicalProductOrderData, BookOrderData, MembershipActivationOrderData, MembershipUpgradeOrderData> orderData =
            dto.PhysicalProductOrderData != null ? dto.PhysicalProductOrderData.ToDomain()
            : dto.BookOrderData != null ? dto.BookOrderData.ToDomain()
            : dto.MembershipActivationOrderData != null ? dto.MembershipActivationOrderData.ToDomain()
            : dto.MembershipUpgradeOrderData!.ToDomain();
        return new Order(dto.Id, dto.ConfirmationDate, dto.AgentName, orderData);
    }
}