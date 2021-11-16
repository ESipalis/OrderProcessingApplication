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

public class BookOrderProcessorTests
{
    private readonly BookOrderProcessor _sut;
    private readonly Mock<IMediator> _mediatorMock;


    public BookOrderProcessorTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _sut = new BookOrderProcessor(_mediatorMock.Object);
    }

    [Fact]
    public void CheckOrderSuitabilityForProcessing_ShouldReturnTrue_WhenItIsBookOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", new BookOrderData("bookName", "authorName", 1234));

        // Act
        bool result = _sut.CheckOrderSuitabilityForProcessing(order);
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void CheckOrderSuitabilityForProcessing_ShouldReturnFalse_WhenItIsNOTBookOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", new PhysicalProductOrderData("123", 1));

        // Act
        bool result = _sut.CheckOrderSuitabilityForProcessing(order);
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task ProcessOrder_ShouldSendGeneratePackingSlipCommands_WhenItIsBookOrder()
    {
        // Arrange
        var physicalProductOrderData = new PhysicalProductOrderData("123", 1);
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", physicalProductOrderData);

        // Act
        await _sut.ProcessOrder(order);
        
        // Assert
        PackingSlip.PackingSlip packingSlip = physicalProductOrderData.GetPackingSlip(order);
        
        var warehouseRecipient = new PackingSlipRecipient(BookOrderProcessor.WarehouseName, BookOrderProcessor.WarehouseAddress);
        _mediatorMock.Verify(x => x.Send(new GeneratePackingSlipCommand(packingSlip, warehouseRecipient), It.IsAny<CancellationToken>()), Times.Once);
        
        var royaltyRecipient = new PackingSlipRecipient(BookOrderProcessor.RoyaltyName, BookOrderProcessor.RoyaltyAddress);
        _mediatorMock.Verify(x => x.Send(new GeneratePackingSlipCommand(packingSlip, royaltyRecipient), It.IsAny<CancellationToken>()), Times.Once);
    }
}