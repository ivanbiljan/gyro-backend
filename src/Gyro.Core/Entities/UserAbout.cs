using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class UserAbout : EntityBase
{
    public string Department { get; set; }

    public string JobTitle { get; set; }

    public string Location { get; set; }
    public int UserId { get; }
}