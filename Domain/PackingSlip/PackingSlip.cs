namespace Domain.PackingSlip;

public record PackingSlip(long OrderId, ICollection<PackingSlipItem> Items)
{
    // Equality members implemented because there is an ICollection property which needs to use SequenceEqual.
    public virtual bool Equals(PackingSlip? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OrderId == other.OrderId && Items.SequenceEqual(other.Items); // Sequence equal is necessary.
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (OrderId.GetHashCode() * 397) ^ Items.GetHashCode();
        }
    }
}