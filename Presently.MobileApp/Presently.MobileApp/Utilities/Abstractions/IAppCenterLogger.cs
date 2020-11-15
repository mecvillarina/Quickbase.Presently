using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presently.MobileApp.Utilities.Abstractions
{
    public interface IAppCenterLogger
    {
        Task Init();
        void LogEvent(string name, IDictionary<string, string> properties = null);
        void LogError(Exception exception, IDictionary<string, string> properties = null);
    }
}
