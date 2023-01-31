using MakoIoT.Device.Services.Interface;
using nanoFramework.DependencyInjection;

namespace MakoIoT.Device.Services.WiFi.Extensions
{
    public static class DeviceBuilderExtension
    {
        public static IDeviceBuilder AddWiFi(this IDeviceBuilder builder)
        {
            builder.Services.AddSingleton(typeof(INetworkProvider), typeof(WiFiNetworkProvider));
            return builder;
        }
    }
}
