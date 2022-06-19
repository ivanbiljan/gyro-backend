using Gyro.Core.Shared;
using Gyro.Core.Shared.Authorization;
using Gyro.Core.Shared.Emails;
using Gyro.Infrastructure.Authorization;
using Gyro.Infrastructure.Emails;
using Gyro.Infrastructure.Persistence;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
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
        serviceCollection.AddDbContext<GyroContext>(opts =>
        {
            if (connectionString == "InMemory")
            {
                opts.UseInMemoryDatabase("InMemory");
            }
            else
            {
                opts.UseSqlServer(connectionString);
            }
        });

        serviceCollection.AddScoped<IGyroContext>(provider =>
        {
            var context = provider.GetRequiredService<GyroContext>();
            context.Database.EnsureCreated();
            
            return context;
        });

        serviceCollection.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.SetDefaultSettings();

            var mailjetSection = configuration.GetSection("Mailjet");
            client.UseBasicAuthentication(mailjetSection["ApiKey"], mailjetSection["ApiSecret"]);
        });

        serviceCollection.AddTransient<IJwtFactory, JwtFactory>();
        serviceCollection.AddTransient<IJwtService, JwtService>();

        serviceCollection.AddTransient<IEmailService, MailjetEmailService>();

        serviceCollection.AddHangfire(hangfireConfiguration =>
        {
            hangfireConfiguration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();

            if (connectionString == "InMemory")
            {
                hangfireConfiguration.UseMemoryStorage();
            }
            else
            {
                hangfireConfiguration.UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                });
            }
        });

        return serviceCollection;
    }
}