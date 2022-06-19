using System.Threading.Tasks;
using System.Xml.Serialization;
using Gyro.Core.Users.Commands;
using Xunit;

namespace Gyro.Core.Tests.Users.Commands;

public sealed class RegisterUserCommandTests
{
    [Theory]
    [InlineData("")]
    [InlineData("email.com")]
    [InlineData("A@b@c@domain.com")]
    [InlineData("a”b(c)d,e:f;gi[j\\k]l@domain.com")]
    [InlineData("test@domain..com")]
    [InlineData(" leading@domain.com")]
    [InlineData("trailing@domain.com ")]
    public async Task RequestValidator_InvalidEmail_ThrowsValidationException(string email)
    {
        var request = new RegisterUserRequest(email, "username", "password", "first name", "last name");
        var validator = new RegisterUserCommandValidator();

        var result = await validator.ValidateAsync(request);

        Assert.Single(result.Errors);
    }

    [Fact]
    public async Task RequestValidator_EmptyUsername_ThrowsValidationException()
    {
        var request = new RegisterUserRequest("email@gmail.com", string.Empty, "password", "first name", "last name");
        var validator = new RegisterUserCommandValidator();

        var result = await validator.ValidateAsync(request);

        Assert.Single(result.Errors);
    }
}