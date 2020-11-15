using Acr.UserDialogs;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Navigation;
using System.Threading.Tasks;

namespace Presently.MobileApp.ViewModels
{
    public class SplashScreenPageViewModel : ViewModelBase
    {
        public SplashScreenPageViewModel(IPageNavigator pageNavigator, ILogger logger, IUserDialogs userDialogs) : base(pageNavigator, logger, userDialogs)
        {
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await Task.Delay(1000);

            await PageNavigator.NavigateAsync($"../{ViewNames.LoginPage}");
        }
    }
}
