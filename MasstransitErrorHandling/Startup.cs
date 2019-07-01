using MassTransit;
using MasstransitErrorHandling;
using MasstransitErrorHandling.Consumers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MasstransitErrorHandling
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<CommonConsumer>(); 
            });
        }
    }
}
