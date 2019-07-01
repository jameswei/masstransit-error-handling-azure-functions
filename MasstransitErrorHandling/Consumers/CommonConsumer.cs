using System;
using System.Threading.Tasks;
using MassTransit;
using MasstransitErrorHandling.Models;

namespace MasstransitErrorHandling.Consumers
{
    public class CommonConsumer : IConsumer<OrderCreated>
    {
        public CommonConsumer()
        {
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            throw new Exception("I should end up in deadletter queue");
        }
    }
}
