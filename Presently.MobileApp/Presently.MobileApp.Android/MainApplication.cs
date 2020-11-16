using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace Presently.MobileApp.Droid
{
    [Application(Theme = "@style/MainTheme")]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
            CrossCurrentActivity.Current.Init(this);
        }
    }
}
