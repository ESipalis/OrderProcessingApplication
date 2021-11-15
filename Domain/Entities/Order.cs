using Domain.ValueObjects;
using OneOf;

namespace Domain.Entities;

public record Order(long Id, DateTimeOffset ConfirmationDate, OneOf<PhysicalProductOrderData, BookOrderData, MembershipActivationOrderData, MembershipUpgradeOrderData> Data);