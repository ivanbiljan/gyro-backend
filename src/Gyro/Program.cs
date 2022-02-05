﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gyro.Application.Extensions;
using Gyro.Infrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gyro
{
    internal sealed class Settings
    {
        public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
    }

    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
            // var builder = WebApplication.CreateBuilder(args);
            // var isDevelopment = builder.Environment.IsDevelopment();
            //
            // builder.Host.UseSerilog(ConfigureLogger);
            //
            // ConfigureServices(builder.Services, builder.Configuration);
            //
            // var app = builder.Build();
            // if (app.Environment.IsDevelopment())
            // {
            //     app.UseSwagger();
            // }
            //
            // await app.RunAsync();

            using var host = BuildWebHost(args);
            await host.RunAsync();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog(ConfigureLogger)
                .Build();
        }

        private static void ConfigureLogger(WebHostBuilderContext context, LoggerConfiguration configuration)
        {
            configuration.WriteTo.Console();
        }
    }
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                .AddInfrastructure(Configuration);

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddLogging(x => x.AddSerilog());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.DisplayRequestDuration());
            }
            
            app.UseCors(opts => opts.AllowAnyOrigin());
        }
    }
}