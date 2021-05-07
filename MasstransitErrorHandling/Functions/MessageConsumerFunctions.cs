using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.WebJobs.ServiceBusIntegration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace MasstransitErrorHandling.Functions
{
    // POCO class 来实现 azure function
    public class MessageConsumerFunctions
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageConsumerFunctions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // 通过 Azure Functions 的 attribute 来定义 function method
        [FunctionName(nameof(MessageConsumerFunction))]
        public async Task MessageConsumerFunction(
            // 定义 trigger 类型，由 asb queue 中的 message 来 trigger
            [ServiceBusTrigger("%QueueName%", Connection = "AzureWebJobsServiceBus")]
            Message message,
            ILogger logger,
            IBinder binder,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"MessageConsumerFunction: consuming message {message.MessageId}");

            // 创建真正来处理消息的 receiver
            var receiver = Bus.Factory.CreateBrokeredMessageReceiver(binder, cfg =>
            {
                cfg.CancellationToken = cancellationToken;
                cfg.SetLog(logger);
                cfg.UseRetry(configurator => configurator.Immediate(5));
                cfg.ConfigureConsumers(_serviceProvider);
            });

            await receiver.Handle(message);

            logger.LogInformation($"Consumed message {message.MessageId}");
        }

    }
}
