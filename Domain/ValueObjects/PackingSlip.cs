namespace Domain.ValueObjects;

public record PackingSlip(long OrderId, ICollection<PackingSlipItem> Items);