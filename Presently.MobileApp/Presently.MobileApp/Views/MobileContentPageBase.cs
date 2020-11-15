using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Presently.MobileApp.Views
{
    public class MobileContentPageBase : ContentPage
    {
        public MobileContentPageBase()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BackgroundColor = Color.White;
        }
    }
}
