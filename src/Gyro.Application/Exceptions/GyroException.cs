using System;

namespace Gyro.Application.Exceptions
{
    public class GyroException : Exception
    {
        public GyroException(string message) : base(message)
        {
        }

        public GyroException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}