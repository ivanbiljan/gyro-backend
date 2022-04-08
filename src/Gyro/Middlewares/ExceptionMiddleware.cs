using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Gyro.Middlewares
{
    internal sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (GyroException gyroException)
            {
                await WriteResponseAsync(HttpStatusCode.BadRequest, gyroException.Message);
            }
            catch (ValidationException validationException)
            {
                var errors = validationException.Errors.Select(e => e.ErrorMessage);
                await WriteResponseAsync(HttpStatusCode.BadRequest, errors);
            }
            catch (Exception ex)
            {
                await WriteResponseAsync(HttpStatusCode.InternalServerError, ex);
            }

            async Task WriteResponseAsync(HttpStatusCode statusCode, object value)
            {
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(value);
            }
        }
    }

    internal static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}