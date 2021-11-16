using Domain.PackingSlip;

namespace Domain.Commands;

public record GeneratePackingSlipCommand(PackingSlip.PackingSlip PackingSlip, PackingSlipRecipient Recipient) : ICommand;