using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Common;
using Domain.Order.Processor.SpecificOrderProcessor;
using Domain.Order.SpecificOrderData;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Domain.Tests.Unit.Order.Processor.SpecificOrderProcessor;

public class AgentCommissionOrderProcessorTests
{
    private readonly AgentCommissionOrderProcessor _sut;
    private readonly Mock<IMediator> _mediatorMock;


    public AgentCommissionOrderProcessorTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _sut = new AgentCommissionOrderProcessor(_mediatorMock.Object);
    }

    [Fact]
    public void CheckOrderSuitabilityForProcessing_ShouldReturnTrue_WhenItIsPhysicalProductOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", new PhysicalProductOrderData("123", 1));

        // Act
        bool result = _sut.CheckOrderSuitabilityForProcessing(order);
        
        // Assert
        result.Should().BeTrue();
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
    public void CheckOrderSuitabilityForProcessing_ShouldReturnFalse_WhenItIsSomeOtherTypeOfOrder()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", new MembershipActivationOrderData("bookName", "authorName", MembershipLevel.Basic));

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
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, "testAgentName", physicalProductOrderData);

        // Act
        await _sut.ProcessOrder(order);
        
        // Assert
        var addAgentCommissionCommand = new AddAgentCommissionCommand(order.AgentName, AgentCommissionOrderProcessor.AgentCommissionValue);
        _mediatorMock.Verify(x => x.Send(addAgentCommissionCommand, It.IsAny<CancellationToken>()), Times.Once);
    }
}