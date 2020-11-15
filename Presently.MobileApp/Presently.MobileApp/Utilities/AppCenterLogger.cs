using Presently.MobileApp.Utilities.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if RELEASE
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Presently.MobileApp.Common.Constants;
#endif

namespace Presently.MobileApp.Utilities
{
    public class AppCenterLogger : IAppCenterLogger
    {
#if RELEASE
        public async Task Init()
        {
            bool isAppCenterEnabled = await AppCenter.IsEnabledAsync();
            if (!isAppCenterEnabled)
            {
                await InitializeAnalyticsReport();
                await InitializeCrashReport();

                string appSecret = $"android={AppConstants.AppCenterAndroidKey};ios={AppConstants.AppCenteriOSKey}";
                AppCenter.Start(appSecret, typeof(Analytics), typeof(Crashes));
            }
        }

        public void LogError(Exception exception, IDictionary<string, string> properties = null)
            => Crashes.TrackError(exception, properties);

        public void LogEvent(string name, IDictionary<string, string> properties = null)
            => Analytics.TrackEvent(name, properties);

        private Task InitializeAnalyticsReport() => Analytics.SetEnabledAsync(true);

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Critical Code Smell",
            "S2696:Instance members should not write to \"static\" fields",
            Justification = "Since this class is registered as a singleton, only one instance of this class will always update `Crashes.ShouldAwaitUserConfirmation`.")]
        private async Task InitializeCrashReport()
        {
            Crashes.SendingErrorReport += (sender, e) =>
            {
                AppCenterLog.Info(AppConstants.AppCenterLogTag, "Sending error report");

                var args = e;
                var report = args.Report;
                LogReport(report, null);
            };

            Crashes.SentErrorReport += (sender, e) =>
            {
                AppCenterLog.Info(AppConstants.AppCenterLogTag, "Sent error report");

                var args = e;
                var report = args.Report;
                LogReport(report, null);
            };

            Crashes.FailedToSendErrorReport += (sender, e) =>
            {
                AppCenterLog.Info(AppConstants.AppCenterLogTag, "Failed to send error report");

                var args = e;
                var report = args.Report;
                LogReport(report, e.Exception);
            };

            Crashes.ShouldAwaitUserConfirmation = () => true;

            await Crashes.SetEnabledAsync(true);
        }

        private void LogReport(ErrorReport report, object exception)
        {
            string message;

            if (!string.IsNullOrEmpty(report.StackTrace))
            {
                message = report.StackTrace;
                AppCenterLog.Verbose(AppConstants.AppCenterLogTag, message);
            }
            else
            {
                AppCenterLog.Verbose(AppConstants.AppCenterLogTag, "No system exception was found");
            }

            if (report.AndroidDetails != null)
            {
                AppCenterLog.Verbose(AppConstants.AppCenterLogTag, report.AndroidDetails.ThreadName);
            }
            else if (report.iOSDetails != null)
            {
                AppCenterLog.Verbose(AppConstants.AppCenterLogTag, report.iOSDetails.ExceptionName);
            }

            if (exception != null)
            {
                AppCenterLog.Verbose(AppConstants.AppCenterLogTag, "There is an exception associated with the failure");
            }
        }

#elif DEBUG
        public Task Init() => Task.CompletedTask;

        public void LogError(Exception exception, IDictionary<string, string> properties = null)
        {
            // Not implemented during DEBUG.
        }

        public void LogEvent(string name, IDictionary<string, string> properties = null)
        {
            // Not implemented during DEBUG.
        }
#endif
    }
}
