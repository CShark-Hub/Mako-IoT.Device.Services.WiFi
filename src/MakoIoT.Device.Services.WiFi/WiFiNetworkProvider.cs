using System.Net.NetworkInformation;
using System.Threading;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.WiFi.Configuration;
using nanoFramework.Networking;

namespace MakoIoT.Device.Services.WiFi
{
    public class WiFiNetworkProvider : INetworkProvider
    {
        private readonly WiFiConfig _wifiConfig;
        private readonly ILog _logger;

        public WiFiNetworkProvider(IConfigurationService configService, ILog logger)
        {
            _wifiConfig = (WiFiConfig)configService.GetConfigSection(WiFiConfig.SectionName, typeof(WiFiConfig));
            _logger = logger;
        }

        public bool IsConnected => WifiNetworkHelper.Status == NetworkHelperStatus.NetworkIsReady;

        public string ClientAddress => NetworkInterface.GetAllNetworkInterfaces()[0].IPv4Address;

        public void Connect()
        {
            if (!IsConnected)
            {
                _logger.Trace($"Network status: {WifiNetworkHelper.Status} - connecting");
                CancellationTokenSource cs = new(_wifiConfig.ConnectionTimeout * 1000);
                bool success = WifiNetworkHelper.ConnectDhcp(_wifiConfig.Ssid, _wifiConfig.Password, requiresDateTime: true, token: cs.Token);
                _logger.Trace($"WifiNetworkHelper.ConnectDhcp result: {success}");
                if (!success)
                {
                    _logger.Error( "Network connection error", WifiNetworkHelper.HelperException);
                }
            }
            _logger.Trace($"Network status: {WifiNetworkHelper.Status}");
        }

        public void Disconnect()
        {
            WifiNetworkHelper.Disconnect();   
        }
    }
}
