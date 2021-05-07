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

        // 定义 MassTransit 中 consumer 的 consume()，接收由 MassTransit 包装过的消息 ConsumerContext
        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            throw new Exception("I should end up in deadletter queue");
        }
    }
}
