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
    public class MessageConsumerFunctions
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageConsumerFunctions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [FunctionName(nameof(MessageConsumerFunction))]
        public async Task MessageConsumerFunction(
            [ServiceBusTrigger("%QueueName%", Connection = "AzureWebJobsServiceBus")]
            Message message,
            ILogger logger,
            IBinder binder,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"MessageConsumerFunction: consuming message {message.MessageId}");

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
