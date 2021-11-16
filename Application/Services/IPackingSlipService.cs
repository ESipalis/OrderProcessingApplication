using Domain.PackingSlip;

namespace Application.Services;

public interface IPackingSlipService
{
    Task SendPackingSlip(PackingSlip packingSlip, string address);
}