using Gyro.Domain.Shared;

namespace Gyro.Domain.Entities
{
    public sealed class Permission : EntityBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}