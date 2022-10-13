using MakoIoT.Device.Services.Configuration.Metadata.Attributes;

namespace MakoIoT.Device.Services.WiFi.Configuration
{
    [SectionMetadata("Wi-Fi")]

    public class WiFiConfig   
    {
        [ParameterMetadata("SSID")]
        public string Ssid { get; set; }
        [ParameterMetadata("Password", isSecret: true)]
        public string Password { get; set; }
        [ParameterMetadata("Connection timeout")]
        public int ConnectionTimeout { get; set; } = 60;

        public static string SectionName => "WiFi";
    }
}