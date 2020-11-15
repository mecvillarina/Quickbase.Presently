using Acr.UserDialogs;
using Presently.MobileApp.Utilities.Abstractions;

namespace Presently.MobileApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(IPageNavigator pageNavigator, 
            ILogger logger, 
            IUserDialogs userDialogs, 
            IRequestExceptionHandler requestExceptionHandler) : base(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
        }


    }
}
