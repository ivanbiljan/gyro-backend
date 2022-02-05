using System.Reflection;
using Gyro.Application.Shared.AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gyro.Application.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddAutoMapper(cfg => cfg.AddProfile(new MainProfile()));
        }
    }
}