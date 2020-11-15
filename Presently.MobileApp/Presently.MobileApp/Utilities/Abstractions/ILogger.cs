using System;
using System.Collections.Generic;

namespace Presently.MobileApp.Utilities.Abstractions
{
    public interface ILogger
    {
        void AppCenterLogEvent(string name, IDictionary<string, string> properties = null);
        void AppCenterLogError(Exception exception, IDictionary<string, string> properties = null);
        void Debug(string message);
        void Error(string message);
        void Error(Exception ex);
        void Info(string message);
        void Write(string tag, string message);
    }
}
