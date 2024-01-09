using System;
using System.Net.NetworkInformation;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.WiFi.Configuration;
using nanoFramework.Networking;

namespace MakoIoT.Device.Services.WiFi
{
    public class WiFiNetworkProvider : INetworkProvider
    {
        private readonly IConfigurationService _configService;
        private readonly ILog _logger;

        public WiFiNetworkProvider(IConfigurationService configService, ILog logger)
        {
            _configService = configService;
            _logger = logger;
        }

        public bool IsConnected => WifiNetworkHelper.Status == NetworkHelperStatus.NetworkIsReady;

        public string ClientAddress => NetworkInterface.GetAllNetworkInterfaces()[0].IPv4Address;

        public void Connect()
        {
            if (IsConnected)
            {
                _logger.Trace($"Network status: {WifiNetworkHelper.Status}");
                return;
            }
            
            var configSection = (WiFiConfig)_configService.GetConfigSection(WiFiConfig.SectionName, typeof(WiFiConfig));
            if (String.IsNullOrEmpty(configSection.Ssid))
            {
                _logger.Error("SSID not configured");
                return;
            }

            _logger.Trace($"Network status: {WifiNetworkHelper.Status} - connecting to {configSection.Ssid}");

            var cs = new DelayCancellationTokenSource(configSection.ConnectionTimeout * 1000);

            var success = WifiNetworkHelper.ConnectDhcp(configSection.Ssid, configSection.Password, requiresDateTime: true, token: cs.Token);
            _logger.Trace($"WifiNetworkHelper.ConnectDhcp result: {success}");

            if (!success)
            {
                _logger.Error( $"Network connection error {WifiNetworkHelper.Status}", WifiNetworkHelper.HelperException);
            }
        }

        public void Disconnect()
        {
            WifiNetworkHelper.Disconnect();   
        }
    }
}
