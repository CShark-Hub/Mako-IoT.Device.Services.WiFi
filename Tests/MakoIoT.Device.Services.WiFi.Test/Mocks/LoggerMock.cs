﻿using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace MakoIoT.Device.Services.WiFi.Test.Mocks
{
    internal class LoggerMock : ILogger
    {
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            throw new NotImplementedException();
        }
    }
}
