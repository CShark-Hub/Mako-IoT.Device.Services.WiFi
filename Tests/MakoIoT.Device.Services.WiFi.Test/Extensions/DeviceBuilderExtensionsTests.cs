using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.WiFi.Extensions;
using MakoIoT.Device.Services.WiFi.Test.Mocks;
using Microsoft.Extensions.Logging;
using nanoFramework.DependencyInjection;
using nanoFramework.TestFramework;

namespace MakoIoT.Device.Services.WiFi.Test.Extensions
{
    [TestClass]
    public class DeviceBuilderExtensionsTests
    {
        [TestMethod]
        public void AddWiFi_Should_RegisterServicesAndReturnDevice()
        {
            var deviceBuilder = new DeviceBuilderMock();
            deviceBuilder.Services.AddSingleton(typeof(IConfigurationService), new ConfigurationServiceMock());
            deviceBuilder.Services.AddSingleton(typeof(ILogger), new LoggerMock());

            DeviceBuilderExtension.AddWiFi(deviceBuilder);
            var serviceProvider = deviceBuilder.Services.BuildServiceProvider();

            var service = serviceProvider.GetService(typeof(INetworkProvider));
            Assert.IsNotNull(service);
        }
    }
}