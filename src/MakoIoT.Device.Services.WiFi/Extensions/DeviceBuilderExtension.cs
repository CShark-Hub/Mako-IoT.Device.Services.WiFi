using MakoIoT.Device.Services.DependencyInjection;
using MakoIoT.Device.Services.Interface;

namespace MakoIoT.Device.Services.WiFi.Extensions
{
    public static class DeviceBuilderExtension
    {
        public static IDeviceBuilder AddWiFi(this IDeviceBuilder builder)
        {
            DI.RegisterSingleton(typeof(INetworkProvider), typeof(WiFiNetworkProvider));
            return builder;
        }
    }
}
