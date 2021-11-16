using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Order.Processor.SpecificOrderProcessor;
using Domain.Order.SpecificOrderData;
using Domain.Order.SpecificOrderData.Extensions;
using Domain.PackingSlip;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Domain.Tests.Unit.Order.Processor.SpecificOrderProcessor;

public class PhysicalProductOrderProcessorTests
{
    private readonly PhysicalProductOrderProcessor _sut;
    private readonly Mock<IMediator> _mediatorMock;


    public PhysicalProductOrderProcessorTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _sut = new PhysicalProductOrderProcessor(_mediatorMock.Object);
    }

    [Fact]
    public void CheckOrderSuitabilityForProcessing_ShouldReturnTrue_WhenItIsPhysicalProductOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new PhysicalProductOrderData("123", 1));

        // Act
        bool result = _sut.CheckOrderSuitabilityForProcessing(order);
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void CheckOrderSuitabilityForProcessing_ShouldReturnFalse_WhenItIsNOTPhysicalProductOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new BookOrderData("bookName", "authorName", 1234));

        // Act
        bool result = _sut.CheckOrderSuitabilityForProcessing(order);
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task ProcessOrder_ShouldSendGeneratePackingSlipCommand_WhenItIsPhysicalProductOrder()
    {
        // Arrange
        var physicalProductOrderData = new PhysicalProductOrderData("123", 1);
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, physicalProductOrderData);

        // Act
        await _sut.ProcessOrder(order);
        
        // Assert
        PackingSlip.PackingSlip packingSlip = physicalProductOrderData.GetPackingSlip(order);
        var packingSlipRecipient = new PackingSlipRecipient(PhysicalProductOrderProcessor.WarehouseName, PhysicalProductOrderProcessor.WarehouseAddress);
        var generatePackingSlipCommand = new GeneratePackingSlipCommand(packingSlip, packingSlipRecipient);
        _mediatorMock.Verify(x => x.Send(generatePackingSlipCommand, It.IsAny<CancellationToken>()), Times.Once);
    }
}