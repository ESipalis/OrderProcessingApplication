using Application.Services;
using Domain.Commands;
using MediatR;

namespace Application.CommandHandlers;

public class GeneratePackingSlipCommandHandler : ICommandHandler<GeneratePackingSlipCommand>
{
    private readonly IPackingSlipService _packingSlipService;

    public GeneratePackingSlipCommandHandler(IPackingSlipService packingSlipService)
    {
        _packingSlipService = packingSlipService;
    }

    public async Task<Unit> Handle(GeneratePackingSlipCommand request, CancellationToken cancellationToken)
    {
        await _packingSlipService.SendPackingSlip(request.PackingSlip, request.Recipient.Address);
        return Unit.Value;
    }
}