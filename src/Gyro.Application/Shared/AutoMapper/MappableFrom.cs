using AutoMapper;

namespace Gyro.Application.Shared.AutoMapper
{
    public abstract class MappableFrom<T>
    {
        public virtual void Map(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}