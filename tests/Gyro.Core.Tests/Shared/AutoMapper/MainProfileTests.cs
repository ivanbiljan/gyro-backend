using System;
using AutoMapper;
using Gyro.Core.Entities;
using Gyro.Core.Shared.AutoMapper;
using Gyro.Core.Users.Queries;
using Xunit;

namespace Gyro.Core.Tests.Shared.AutoMapper;

public sealed class MainProfileTests
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMapper _mapper;

    public MainProfileTests()
    {
        _configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile(new MainProfile()));
        _mapper = new Mapper(_configurationProvider);
    }

    [Theory]
    [InlineData(typeof(User), typeof(UserDto))]
    public void AssertDtoMappingsAreCorrect(Type sourceType, Type destinationType)
    {
        _mapper.Map(sourceType, destinationType);
    }

    [Fact]
    public void AssertConfigurationIsValid()
    {
        _configurationProvider.AssertConfigurationIsValid();
    }
}