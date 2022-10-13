using System;
using System.Net.NetworkInformation;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.WiFi.Configuration;
using MakoIoT.Device.Services.WiFi.Interface;

namespace MakoIoT.Device.Services.WiFi
{
    public class WiFiInterfaceManager : INetworkInterfaceManager
    {
        private readonly NetworkInterface _wifiInterface;
        private readonly NetworkInterface _apInterface;
        private readonly Wireless80211Configuration _wifiConfiguration;
        private readonly WirelessAPConfiguration _apConfiguration;
        private readonly WiFiAPConfig _config;
        private DhcpServer _dhcpServer;

        public WiFiInterfaceManager(IConfigurationService configService)
        {
            _config = (WiFiAPConfig)configService.GetConfigSection(WiFiAPConfig.SectionName, typeof(WiFiAPConfig));

            FindInterfaces(out _wifiInterface, out _apInterface);
            _wifiConfiguration = Wireless80211Configuration.GetAllWireless80211Configurations()[_wifiInterface.SpecificConfigId];
            _apConfiguration = WirelessAPConfiguration.GetAllWirelessAPConfigurations()[_apInterface.SpecificConfigId];

            if (_config.EnableDhcp && IsApEnabled)
                InitializeDhcp();
        }

        public bool IsWifiEnabled => (_wifiConfiguration.Options & Wireless80211Configuration.ConfigurationOptions.Enable)
            == Wireless80211Configuration.ConfigurationOptions.Enable;

        public bool IsApEnabled => (_apConfiguration.Options & WirelessAPConfiguration.ConfigurationOptions.Enable)
            == WirelessAPConfiguration.ConfigurationOptions.Enable;

        public void EnableWiFi()
        {
            _wifiConfiguration.Options = Wireless80211Configuration.ConfigurationOptions.Enable;
            _wifiConfiguration.SaveConfiguration();
        }

        public void DisableWiFi()
        {
            _wifiConfiguration.Options = Wireless80211Configuration.ConfigurationOptions.Disable;
            _wifiConfiguration.SaveConfiguration();
        }

        public void EnableAP()
        {
            _apInterface.EnableStaticIPv4(_config.IpAddress, _config.SubnetMask, _config.IpAddress);
            _apConfiguration.Ssid = _config.Ssid;
            if (String.IsNullOrEmpty(_config.Password))
                _apConfiguration.Authentication = AuthenticationType.Open;
            else
            {
                _apConfiguration.Authentication = AuthenticationType.WPA2;
                _apConfiguration.Password = _config.Password;
            }

            _apConfiguration.MaxConnections = _config.MaxConnections;

            _apConfiguration.Options = WirelessAPConfiguration.ConfigurationOptions.Enable |
                                       WirelessAPConfiguration.ConfigurationOptions.AutoStart;
            _apConfiguration.SaveConfiguration();
        }

        public void DisableAP()
        {
            _apConfiguration.Options = WirelessAPConfiguration.ConfigurationOptions.Disable;
            _apConfiguration.SaveConfiguration();
        }

        public void InitializeDhcp()
        {

        }

        private void FindInterfaces(out NetworkInterface wifiInterface, out NetworkInterface apInterface)
        {
            wifiInterface = null;
            apInterface = null;
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                switch (networkInterface.NetworkInterfaceType)
                {
                    case NetworkInterfaceType.Wireless80211:
                        wifiInterface = networkInterface;
                        break;
                    case NetworkInterfaceType.WirelessAP:
                        apInterface = networkInterface;
                        break;
                }
            }
        }
    }
}
