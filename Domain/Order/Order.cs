using Domain.Order.SpecificOrderData;
using OneOf;

namespace Domain.Order;

public record Order(long Id, DateTimeOffset ConfirmationDate, string AgentName, OneOf<PhysicalProductOrderData, BookOrderData, MembershipActivationOrderData, MembershipUpgradeOrderData> Data);