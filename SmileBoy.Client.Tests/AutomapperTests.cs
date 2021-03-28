using AutoMapper;
using SmileBoyClient;
using Xunit;

namespace SmileBoy.Client.Tests
{
    public class AutomapperTests
    {
        [Fact]
        public void CorrectlyConfigurated()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>());

            config.AssertConfigurationIsValid();
        }
    }
}
