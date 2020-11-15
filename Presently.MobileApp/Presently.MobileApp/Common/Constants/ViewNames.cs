namespace Presently.MobileApp.Common.Constants
{
    public static class ViewNames
    {
        public const string NavigationPage = nameof(NavigationPage);
        public const string SplashScreenPage = nameof(SplashScreenPage);
        public const string MainPage = nameof(MainPage);
        public const string LoginPage = nameof(LoginPage);
        public const string MainMasterDetailPage = nameof(MainMasterDetailPage);
        public const string AccountPage = nameof(AccountPage);
        public const string AttendanceLogPage = nameof(AttendanceLogPage);
        public const string ClockPage = nameof(ClockPage);

        public static string GetMainMasterPage()
        {
            return $"{MainMasterDetailPage}/{NavigationPage}/{AccountPage}";
        }

        public static string ResetLandingPage()
        {
            return $"/{NavigationPage}/{SplashScreenPage}";
        }
    }
}
