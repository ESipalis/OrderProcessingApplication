namespace Domain.ValueObjects;

public record MembershipUpgradeOrderData(long MembershipId, MembershipLevel NewLevel);