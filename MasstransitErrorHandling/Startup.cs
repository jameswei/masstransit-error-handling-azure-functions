using MassTransit;
using MasstransitErrorHandling;
using MasstransitErrorHandling.Consumers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MasstransitErrorHandling
{
    // 直接继承 FunctionStartup class，通过重写 Configure() 配置 MassTransit
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMassTransit(configurator =>
            {
                // 向 MassTransit 注册 IConsumer 接口的 consumer 实现类
                configurator.AddConsumer<CommonConsumer>(); 
            });
        }
    }
}
