using Gyro.Core.Shared;
using Gyro.Core.Shared.Authorization;
using Gyro.Core.Shared.Emails;
using Gyro.Infrastructure.Authorization;
using Gyro.Infrastructure.Emails;
using Gyro.Infrastructure.Persistence;
using Mailjet.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gyro.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(GyroContext));
        serviceCollection.AddDbContext<GyroContext>(opts => opts.UseInMemoryDatabase("InMemory"));

        serviceCollection.AddScoped<IGyroContext>(provider =>
        {
            var context = provider.GetRequiredService<GyroContext>();
            return context;
        });

        serviceCollection.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.SetDefaultSettings();

            var mailjetSection = configuration.GetSection("Mailjet");
            client.UseBasicAuthentication(mailjetSection["ApiKey"], mailjetSection["ApiSecret"]);
        });

        serviceCollection.AddTransient<IJwtService, JwtService>();

        serviceCollection.AddTransient<IEmailService, MailjetEmailService>();

        return serviceCollection;
    }
}