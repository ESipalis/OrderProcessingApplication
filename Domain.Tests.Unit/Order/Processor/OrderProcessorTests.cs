using System;
using System.Collections.Generic;
using Domain.Order.Processor;
using Domain.Order.Processor.SpecificOrderProcessor;
using Domain.Order.SpecificOrderData;
using Moq;
using Xunit;

namespace Domain.Tests.Unit.Order.Processor;

public class OrderProcessorTests
{
    private readonly Mock<ISpecificOrderProcessor>[] _specificOrderProcessorMocks = new Mock<ISpecificOrderProcessor>[3];
    private readonly OrderProcessor _sut;

    public OrderProcessorTests()
    {
        for (var a = 0; a < _specificOrderProcessorMocks.Length; a++)
        {
            _specificOrderProcessorMocks[a] = new Mock<ISpecificOrderProcessor>();
        }

        _sut = new OrderProcessor(new[] { _specificOrderProcessorMocks[0].Object, _specificOrderProcessorMocks[1].Object, _specificOrderProcessorMocks[2].Object });
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void ProcessOrder_ShouldExecuteMatchingProcessors_WhenOrderMatchesOnlyOneProcessor(params bool[] orderSuitabilityResults)
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new PhysicalProductOrderData("123", 1));
        SetupISpecificOrderProcessorMocksForSuitabilityResults(order, orderSuitabilityResults);

        // Act
        _sut.ProcessOrder(order);

        // Assert
        for (var index = 0; index < orderSuitabilityResults.Length; index++)
        {
            bool orderSuitabilityResult = orderSuitabilityResults[index];
            Times expectedNumberOfTimesCalled = orderSuitabilityResult ? Times.Once() : Times.Never();
            _specificOrderProcessorMocks[index].Verify(x => x.ProcessOrder(order), expectedNumberOfTimesCalled);
        }
    }
    
    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, true)]
    [InlineData(true, false, true)]
    public void ProcessOrder_ShouldExecuteMatchingProcessors_WhenOrderMatchesMoreThanOneProcessor(params bool[] orderSuitabilityResults)
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new PhysicalProductOrderData("123", 1));
        SetupISpecificOrderProcessorMocksForSuitabilityResults(order, orderSuitabilityResults);

        // Act
        _sut.ProcessOrder(order);

        // Assert
        for (var index = 0; index < orderSuitabilityResults.Length; index++)
        {
            bool orderSuitabilityResult = orderSuitabilityResults[index];
            Times expectedNumberOfTimesCalled = orderSuitabilityResult ? Times.Once() : Times.Never();
            _specificOrderProcessorMocks[index].Verify(x => x.ProcessOrder(order), expectedNumberOfTimesCalled);
        }
    }

    [Fact]
    public void ProcessOrder_ShouldNOTExecuteAnyProcessors_WhenOrderDoesNotMatchAnyProcessors()
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new PhysicalProductOrderData("123", 1));
        SetupISpecificOrderProcessorMocksForSuitabilityResults(order, new []{false, false, false});

        // Act
        _sut.ProcessOrder(order);

        // Assert
        foreach (Mock<ISpecificOrderProcessor> mock in _specificOrderProcessorMocks)
        {
            mock.Verify(x => x.ProcessOrder(order), Times.Never);
        }
    }
    
    
    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, false)]
    [InlineData(false, true, true)]
    [InlineData(true, false, true)]
    [InlineData(false, false, false)]
    public void ProcessOrder_ShouldExecuteMatchingProcessors_WhenOrderMatchesProcessor_MultipleIterations(params bool[] orderSuitabilityResults)
    {
        // Arrange
        var order = new Domain.Order.Order(1, DateTimeOffset.Now, new PhysicalProductOrderData("123", 1));
        SetupISpecificOrderProcessorMocksForSuitabilityResults(order, orderSuitabilityResults);

        // Act
        _sut.ProcessOrder(order);
        _sut.ProcessOrder(order); // Executed the 2nd time on purpose.

        // Assert
        for (var index = 0; index < orderSuitabilityResults.Length; index++)
        {
            bool orderSuitabilityResult = orderSuitabilityResults[index];
            Times expectedNumberOfTimesCalled = orderSuitabilityResult ? Times.Exactly(2) : Times.Never();
            _specificOrderProcessorMocks[index].Verify(x => x.ProcessOrder(order), expectedNumberOfTimesCalled);
        }
    }
    

    private void SetupISpecificOrderProcessorMocksForSuitabilityResults(Domain.Order.Order order, IReadOnlyList<bool> suitabilityResults)
    {
        for (var index = 0; index < _specificOrderProcessorMocks.Length; index++)
        {
            Mock<ISpecificOrderProcessor> mock = _specificOrderProcessorMocks[index];
            bool orderSuitabilityResult = suitabilityResults[index]; // Assuming that inline data will always have the same length.
            mock
                .Setup(x => x.CheckOrderSuitabilityForProcessing(order))
                .Returns(orderSuitabilityResult);
        }
    }
}