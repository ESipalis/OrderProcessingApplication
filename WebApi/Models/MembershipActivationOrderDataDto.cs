using Domain.Common;
using Domain.Order.SpecificOrderData;

namespace WebApi.Models;

public record MembershipActivationOrderDataDto(string FirstName, string LastName, MembershipLevel Level);

public static class MembershipActivationOrderDataDtoExtensions
{
    public static MembershipActivationOrderData ToDomain(this MembershipActivationOrderDataDto dto)
    {
        return new MembershipActivationOrderData(dto.FirstName, dto.LastName, dto.Level);
    }
}