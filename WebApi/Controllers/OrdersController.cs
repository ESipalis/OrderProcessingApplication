using Domain.Order;
using Domain.Order.Processor;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderProcessor _orderProcessor;

    public OrdersController(OrderProcessor orderProcessor)
    {
        _orderProcessor = orderProcessor;
    }

    [HttpPost("process", Name = "ProcessOrder")]
    public async Task<IActionResult> ProcessOrder(ProcessOrderDto processOrderDto)
    {
        // TODO: Add some FluentValidation for the ProcessOrderDto.
        Order order = processOrderDto.ToDomain();
        await _orderProcessor.ProcessOrder(order);
        return NoContent();
    }
}