using Application.Services;
using Domain.Order.Processor;
using Domain.Order.Processor.SpecificOrderProcessor;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<OrderProcessor>();
        services.AddSingleton<ISpecificOrderProcessor, PhysicalProductOrderProcessor>();
        services.AddSingleton<ISpecificOrderProcessor, BookOrderProcessor>();
        services.AddSingleton<ISpecificOrderProcessor, AgentCommissionOrderProcessor>();
        
        services.AddSingleton<IPackingSlipService, DefaultPackingSlipService>();
        services.AddSingleton<IAgentCommissionService, DefaultAgentCommissionService>();
        services.AddMediatR(typeof(IPackingSlipService)); // Adds all handlers from Application layer.
        return services;
    }
}