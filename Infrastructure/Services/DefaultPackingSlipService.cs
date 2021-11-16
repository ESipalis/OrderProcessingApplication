using Application.Services;
using Domain.PackingSlip;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class DefaultPackingSlipService : IPackingSlipService
{
    private readonly ILogger<DefaultPackingSlipService> _logger;

    public DefaultPackingSlipService(ILogger<DefaultPackingSlipService> logger)
    {
        _logger = logger;
    }

    public Task SendPackingSlip(PackingSlip packingSlip, string address)
    {
        _logger.LogTrace("Sending packing slip to {PackingSlipDestinationAddress}", address);
        return Task.CompletedTask;
    }
}