using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Gyro.Application.Shared.AutoMapper
{
    public sealed class MainProfile : Profile
    {
        public MainProfile()
        {
            var mapMethod = typeof(MappableFrom<>).GetMethod(nameof(MappableFrom<object>.Map))!;
            var mappableObjects = from type in Assembly.GetExecutingAssembly().GetExportedTypes()
                where typeof(MappableFrom<>).IsAssignableFrom(type) && !type.IsGenericTypeDefinition
                select Activator.CreateInstance(typeof(MappableFrom<>).MakeGenericType(type));
            foreach (var mappableObject in mappableObjects)
            {
                mapMethod.Invoke(mappableObject, new object?[] { this });
            }
        }
    }
}