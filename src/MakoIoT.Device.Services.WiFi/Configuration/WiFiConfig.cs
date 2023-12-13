namespace MakoIoT.Device.Services.WiFi.Configuration
{
    public class WiFiConfig   
    {
        public string Ssid { get; set; }
        public string Password { get; set; }
        public int ConnectionTimeout { get; set; } = 60;

        public static string SectionName => "WiFi";
    }
}