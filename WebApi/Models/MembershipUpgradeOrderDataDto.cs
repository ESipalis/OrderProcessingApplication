using Domain.Common;
using Domain.Order.SpecificOrderData;

namespace WebApi.Models;

public record MembershipUpgradeOrderDataDto(long MembershipId, MembershipLevel NewLevel);

public static class MembershipUpgradeOrderDataDtoExtensions
{
    public static MembershipUpgradeOrderData ToDomain(this MembershipUpgradeOrderDataDto dto)
    {
        return new MembershipUpgradeOrderData(dto.MembershipId, dto.NewLevel);
    }
}