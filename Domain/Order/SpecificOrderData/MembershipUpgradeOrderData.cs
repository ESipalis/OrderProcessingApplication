using Domain.Common;

namespace Domain.Order.SpecificOrderData;

public record MembershipUpgradeOrderData(long MembershipId, MembershipLevel NewLevel);