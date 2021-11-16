namespace Domain.PackingSlip;

public record PackingSlip(long OrderId, ICollection<PackingSlipItem> Items);