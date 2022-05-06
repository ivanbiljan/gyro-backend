using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Gyro.Core.Shared.AutoMapper;

public sealed class MainProfile : Profile
{
    public MainProfile()
    {
        var mappableTypes = from type in Assembly.GetExecutingAssembly().GetExportedTypes()
            where type.BaseType is {IsGenericType: true} &&
                  typeof(MapsTo<>).IsAssignableFrom(type.BaseType.GetGenericTypeDefinition())
            select type;
        foreach (var mappableType in mappableTypes)
        {
            var instance = Activator.CreateInstance(mappableType);
            var mapMethod = mappableType.GetMethod(nameof(MapsTo<object>.Map))!;
            mapMethod.Invoke(instance, new object?[] {this});
        }
    }
}