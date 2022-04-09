using Gyro.Core.Authorization;
using Gyro.Core.Shared;
using Gyro.Core.Shared.AutoMapper;
using Gyro.Core.Shared.MediatrPipelineBehaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gyro.Core.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddAutoMapper(cfg => cfg.AddProfile(new MainProfile()))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                .AddTransient<IPasswordHasher, PasswordHasher>()
                .AddTransient<IJwtService, JwtService>();
        }
    }
}