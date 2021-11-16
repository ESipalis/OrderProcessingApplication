using Domain.Common;

namespace Domain.Order.SpecificOrderData;

public record MembershipActivationOrderData(string FirstName, string LastName, MembershipLevel Level);