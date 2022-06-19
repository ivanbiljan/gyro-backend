using Gyro.Core.Extensions;
using Gyro.HangfireWorker;
using Gyro.Infrastructure.Extensions;
using Hangfire;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddApplication();
        services.AddInfrastructure(context.Configuration);

        services.AddHangfireServer();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();