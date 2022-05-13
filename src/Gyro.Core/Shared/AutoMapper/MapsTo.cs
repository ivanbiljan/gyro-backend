using AutoMapper;

namespace Gyro.Core.Shared.AutoMapper;

public abstract class MapsTo<T>
{
    public virtual void Map(Profile profile) => profile.CreateMap(typeof(T), GetType());
}