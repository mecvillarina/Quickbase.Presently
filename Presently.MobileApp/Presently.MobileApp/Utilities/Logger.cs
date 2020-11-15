using Presently.MobileApp.Utilities.Abstractions;
using System;
using System.Collections.Generic;

namespace Presently.MobileApp.Utilities
{
    public class Logger : ILogger
    {
        private readonly IAppCenterLogger _appCenterLogger;
        private readonly IDebugLogger _debugLogger;

        public Logger(IAppCenterLogger appCenterLogger, IDebugLogger debugLogger)
        {
            _appCenterLogger = appCenterLogger;
            _debugLogger = debugLogger;
        }

        public void AppCenterLogEvent(string name, IDictionary<string, string> properties = null) => _appCenterLogger.LogEvent(name, properties);

        public void AppCenterLogError(Exception exception, IDictionary<string, string> properties = null) => _appCenterLogger.LogError(exception, properties);

        public void Debug(string message) => _debugLogger.Debug(message);

        public void Error(string message) => _debugLogger.Error(message);

        public void Error(Exception ex) => _debugLogger.Error(ex);

        public void Info(string message) => _debugLogger.Info(message);

        public void Write(string tag, string message) => _debugLogger.Write(tag, message);
    }
}
