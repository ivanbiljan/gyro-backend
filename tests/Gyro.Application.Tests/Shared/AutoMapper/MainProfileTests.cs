using System;
using AutoMapper;
using Gyro.Application.Shared.AutoMapper;
using Gyro.Application.Users.Queries;
using Gyro.Domain.Entities;
using Xunit;

namespace Gyro.Application.Tests.Shared.AutoMapper
{
    public sealed class MainProfileTests
    {
        public MainProfileTests()
        {
            _configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile(new MainProfile()));
            _mapper = new Mapper(_configurationProvider);
        }

        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        [Theory]
        [InlineData(typeof(User), typeof(UserDto))]
        public void AssertDtoMappingsAreCorrect(Type sourceType, Type destinationType)
        {
            Assert.False(true);
            _mapper.Map(sourceType, destinationType);
        }

        [Fact]
        public void AssertConfigurationIsValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }
    }
}