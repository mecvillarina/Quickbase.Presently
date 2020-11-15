using Acr.UserDialogs;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Managers;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Mappers;
using Presently.MobileApp.Repositories;
using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.Database;
using Presently.MobileApp.Utilities;
using Presently.MobileApp.Utilities.Abstractions;
using Presently.MobileApp.ViewModels;
using Presently.MobileApp.Views;
using Presently.MobileApp.WebServices;
using Presently.MobileApp.WebServices.Abstractions;
using Presently.MobileApp.WebServices.Utilities;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Presently.MobileApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{ViewNames.NavigationPage}/{ViewNames.SplashScreenPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterPlugins(containerRegistry);
            RegisterUtilites(containerRegistry);
            RegisterRepositories(containerRegistry);
            RegisterWebServices(containerRegistry);
            RegisterManagers(containerRegistry);
            RegisterUI(containerRegistry);
        }

        private void RegisterUI(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SplashScreenPage, SplashScreenPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage, AccountPageViewModel>();
            containerRegistry.RegisterForNavigation<AttendanceLogPage, AttendanceLogPageViewModel>();
            containerRegistry.RegisterForNavigation<ClockPage, ClockPageViewModel>();
        }

        private void RegisterManagers(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppManager, AppManager>();
            containerRegistry.RegisterSingleton<IInternalAuthManager, InternalAuthManager>();
            containerRegistry.RegisterSingleton<IAuthManager, AuthManager>();
            containerRegistry.RegisterSingleton<IAppUserManager, AppUserManager>();
            containerRegistry.RegisterSingleton<IEmployeeManager, EmployeeManager>();
            containerRegistry.RegisterSingleton<IAttendanceLogManager, AttendanceLogManager>();
            containerRegistry.RegisterSingleton<ILocationManager, LocationManager>();
        }

        private void RegisterWebServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IHttpService, HttpService>();
            containerRegistry.Register<IAppHttpClient, AppHttpClient>();
            containerRegistry.RegisterSingleton<IAuthWebService, AuthWebService>();
            containerRegistry.RegisterSingleton<IAttendanceLogWebService, AttendanceLogWebService>();
            containerRegistry.RegisterSingleton<IEmployeeWebService, EmployeeWebService>();
            containerRegistry.RegisterSingleton<ILocationWebService, LocationWebService>();
        }

        private void RegisterRepositories(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMobileDatabase, MobileDatabase>();
            containerRegistry.RegisterSingleton<IKeyStoreRepository, KeyStoreRepository>();
            containerRegistry.RegisterSingleton<IInternalAuthRepository, InternalAuthRepository>();
            containerRegistry.RegisterSingleton<IAppUserRepository, AppUserRepository>();
            containerRegistry.RegisterSingleton<IEmployeeSiteRepository, EmployeeSiteRepository>();
            containerRegistry.RegisterSingleton<IAttendanceLogRepository, AttendanceLogRepository>();
        }

        private void RegisterUtilites(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IConnectivity, ConnectivityImplementation>();
            containerRegistry.RegisterSingleton<IPermissions, PermissionsImplementation>();
            containerRegistry.RegisterSingleton<IGeolocation, GeolocationImplementation>();
            containerRegistry.RegisterSingleton<IPreferences, PreferencesImplementation>();
            containerRegistry.RegisterSingleton<ISecureStorage, SecureStorageImplementation>();
            containerRegistry.RegisterSingleton<ILogger, Logger>();
            containerRegistry.RegisterSingleton<IHttpMessageHelper, HttpMessageHelper>();
            containerRegistry.RegisterSingleton<IJsonHelper, JsonHelper>();
            containerRegistry.RegisterSingleton<IRequestExceptionHandler, RequestExceptionHandler>();

            containerRegistry.RegisterSingleton<IServiceMapper, ServiceMapper>();
            containerRegistry.RegisterSingleton<IAppCenterLogger, AppCenterLogger>();
            containerRegistry.RegisterSingleton<IDebugLogger, DebugLogger>();
            containerRegistry.Register<IPageNavigator, AppPageNavigator>();
        }

        private void RegisterPlugins(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(UserDialogs.Instance);
        }
    }
}