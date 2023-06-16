# Mako-IoT.Device.Services.WiFi
Manages WiFi connections and interfaces.

## Usage
### Connect to WiFi network (STAtion mode)
Add WiFi configuration in [_DeviceBuilder_](https://github.com/CShark-Hub/Mako-IoT.Device)
```c#
public static void Main()
{
    DeviceBuilder.Create()
        .AddWiFi()
        .AddConfiguration(cfg =>
        {
            cfg.WriteDefault(WiFiConfig.SectionName, new WiFiConfig
            {
                Ssid = "MyWiFiNetwork",
                Password = "MyWiFiPassword"
            });
        })
        .AddFileStorage()
        .Build()
        .Start();

    Thread.Sleep(Timeout.Infinite);
}
```
Use _INetworkProvider_ to connect to thew network
```c#
public class MyAppService : IMyAppService
{
    private readonly INetworkProvider _networkProvider;
    private readonly ILogger _logger;

    public MyAppService(INetworkProvider networkProvider, ILogger logger)
    {
        _networkProvider = networkProvider;
        _logger = logger;
    }

    public void DoSomeNetworking()
    {
        if (!_networkProvider.IsConnected)
        {
            _logger.LogDebug("Network not connected");
            _networkProvider.Connect();
            if (!_networkProvider.IsConnected)
                throw new Exception("Could not connect to network");
        }

        _logger.LogDebug("Connected to WIFI");
        
        //[...]
    }
}
```
