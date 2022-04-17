using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Gyro.Configuration;
using Gyro.Core.Extensions;
using Gyro.Core.Shared.Authorization;
using Gyro.Core.Shared.Emails;
using Gyro.Core.Shared.MediatrPipelineBehaviours;
using Gyro.Core.Users;
using Gyro.Infrastructure.Extensions;
using Gyro.Middlewares;
using Gyro.Services;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gyro
{
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
            using var scope = host.Services.CreateScope();
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
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            
            services.AddApplication()
                .AddInfrastructure(Configuration)
                .AddMediatR(Assembly.GetExecutingAssembly(), typeof(Core.Shared.IGyroContext).Assembly)
                .AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddMvc()
                .AddFluentValidation(config =>
                    config.RegisterValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly))
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);    

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader =
                    ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader()
                    );
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.Configure<MailjetSettings>(Configuration.GetSection("Mailjet"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))   
                };
            });

            services.AddLogging(builder => builder.AddSerilog());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DisplayRequestDuration();
                    
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }
            
            app.UseExceptionWrapper();

            app.UseRouting();
            app.UseCors(opts => opts.AllowAnyOrigin());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context => context.Response.WriteAsync("OK"));
                endpoints.MapControllers();
            });
        }
    }
}