using System;
using Gyro.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gyro
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
        }
    }
}