using Gyro.Core.Shared;
using Gyro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace Gyro.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(GyroContext));
            serviceCollection.AddDbContext<GyroContext>(opts => opts.UseInMemoryDatabase("InMemory"));

            serviceCollection.AddScoped<IGyroContext>(provider =>
            {
                var context = provider.GetRequiredService<GyroContext>();
                return context;
            });
            
            serviceCollection.AddSendGrid(options =>
            {
                options.ApiKey = configuration.GetSection("SendGrid")["ApiKey"];
            });

            return serviceCollection;
        }
    }
}