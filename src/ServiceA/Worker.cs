using Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceA;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = serviceScopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBus>();

                await service.Publish(new OrderCreatedEvent
                {
                    OrderId = Guid.NewGuid()
                }, stoppingToken);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}