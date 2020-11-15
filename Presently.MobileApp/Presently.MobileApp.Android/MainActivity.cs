using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Presently.MobileApp.Droid.Database;
using Presently.MobileApp.Droid.Maps;
using Presently.MobileApp.Repositories.Abstractions;
using Prism;
using Prism.Ioc;
using Xamarin.Forms.GoogleMaps.Android;

namespace Presently.MobileApp.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Presently.MobileApp.App _app;
        public Presently.MobileApp.App App
        {
            get => _app;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            InitLibraries(savedInstanceState);
            InitializeContainer();
            LoadApplication(_app);
        }

        private void InitLibraries(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            UserDialogs.Init(this);
            AiForms.Effects.Droid.Effects.Init();

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Xamarin.FormsGoogleMapsBindings.Init();

            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
        }

        private void InitializeContainer()
        {
            _app = new App(new AndroidInitializer());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations

            containerRegistry.RegisterSingleton<ISQLiteConnectionFactory, AndroidSqlite>();
        }
    }
}

