using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Gyro.Application.Shared.AutoMapper
{
    public sealed class MainProfile : Profile
    {
        public MainProfile()
        {
            var mappableTypes = from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                where type.BaseType is { IsGenericType: true } &&
                      typeof(MappableFrom<>).IsAssignableFrom(type.BaseType.GetGenericTypeDefinition())
                select type;
            foreach (var mappableType in mappableTypes)
            {
                var instance = Activator.CreateInstance(mappableType);
                var mapMethod = mappableType.GetMethod(nameof(MappableFrom<object>.Map))!;
                mapMethod.Invoke(instance, new object?[] { this });
            }
        }
    }
}