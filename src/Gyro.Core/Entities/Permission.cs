using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class Permission : EntityBase
{
    public string Description { get; set; }
    public string Name { get; set; }

    public List<Role> Roles { get; set; }
}