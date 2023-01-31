using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.WiFi.Configuration;
using System;

namespace MakoIoT.Device.Services.WiFi.Test.Mocks
{
    internal class ConfigurationServiceMock : IConfigurationService
    {
        public WiFiConfig WiFiConfigSection { get; } = new WiFiConfig();
        public event EventHandler ConfigurationUpdated;

        public bool ClearAll()
        {
            throw new NotImplementedException();
        }

        public object GetConfigSection(string sectionName, Type objectType)
        {
            if (sectionName == WiFiConfig.SectionName)
            {
                return WiFiConfigSection;
            }

            throw new NotImplementedException();
        }

        public string[] GetSections()
        {
            throw new NotImplementedException();
        }

        public string LoadConfigSection(string sectionName)
        {
            throw new NotImplementedException();
        }

        public void UpdateConfigSection(string sectionName, object section)
        {
            throw new NotImplementedException();
        }

        public bool UpdateConfigSectionString(string sectionName, string sectionString)
        {
            throw new NotImplementedException();
        }

        public bool UpdateConfigSectionString(string sectionName, string sectionString, Type objectType)
        {
            throw new NotImplementedException();
        }

        public void WriteDefault(string sectionName, object section, bool overwrite = false)
        {
            throw new NotImplementedException();
        }
    }
}
