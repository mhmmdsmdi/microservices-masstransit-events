using Contracts;
using MassTransit;

namespace ServiceC;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation($"New order created with {context.Message.OrderId} id.");
        return Task.CompletedTask;
    }
}