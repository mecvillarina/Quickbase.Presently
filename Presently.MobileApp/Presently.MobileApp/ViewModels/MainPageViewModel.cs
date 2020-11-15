using Presently.MobileApp.Utilities.Abstractions;

namespace Presently.MobileApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IPageNavigator pageNavigator, ILogger logger) : base(pageNavigator, logger)
        {
            Title = "Home";
        }
    }
}
