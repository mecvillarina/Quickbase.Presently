namespace Presently.MobileApp.Common.Constants
{
    public static class ViewNames
    {
        public const string NavigationPage = nameof(NavigationPage);
        public const string SplashScreenPage = nameof(SplashScreenPage);
        public const string MainPage = nameof(MainPage);
        public const string LoginPage = nameof(LoginPage);
        public const string MainMasterDetailPage = nameof(MainMasterDetailPage);

        public static string GetMainMasterPage()
        {
            return $"{MainMasterDetailPage}/{NavigationPage}/{MainPage}";
        }

        public static string ResetLandingPage()
        {
            return $"/{NavigationPage}/{SplashScreenPage}";
        }
    }
}
