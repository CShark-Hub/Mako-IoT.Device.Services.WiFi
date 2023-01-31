using MakoIoT.Device.Services.Interface;
using nanoFramework.DependencyInjection;
using System;

namespace MakoIoT.Device.Services.WiFi.Test.Mocks
{
    internal class DeviceBuilderMock : IDeviceBuilder
    {
        public IServiceCollection Services { get; }

        public ConfigureDefaultsDelegate ConfigureDefaultsAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event DeviceStartingDelegate DeviceStarting;
        public event DeviceStoppedDelegate DeviceStopped;

        public DeviceBuilderMock()
        {
            Services = new ServiceCollection();
        }

        public IDevice Build()
        {
            throw new NotImplementedException();
        }

        public IDeviceBuilder ConfigureDI(Action configureDiAction)
        {
            throw new NotImplementedException();
        }
    }
}
