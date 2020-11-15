using Presently.MobileApp.Utilities.Abstractions;
using System;
using System.Linq;

namespace Presently.MobileApp.Utilities
{
    public class DebugLogger : IDebugLogger
    {
        public void Debug(string message) => System.Diagnostics.Debug.Print(GetFormattedMessage("ERROR", message));

        public void Error(string message) => System.Diagnostics.Debug.Print(GetFormattedMessage("ERROR", message));

        public void Error(Exception ex) => Debug(ex.ToString());

        public void Info(string message) => System.Diagnostics.Debug.Print(GetFormattedMessage("INFO", message));

        public void Write(string tag, string message) => System.Diagnostics.Debug.Print(GetFormattedMessage(tag, message));

        private string GetFormattedMessage(string tag, string message)
        {
            var prefix = DateTime.Now.ToString($"MM-dd-yyyy HH:mm:ss.fff");

            message = message.Trim();

            if (!char.IsPunctuation(message.LastOrDefault()))
                message = $"{message}.";

            return prefix + $" ({tag}): {message}";
        }
    }
}