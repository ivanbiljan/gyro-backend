using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class UserAbout : EntityBase
{
    public int UserId { get; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    public string Location { get; set; }
}