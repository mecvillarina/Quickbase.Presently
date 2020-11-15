using Acr.UserDialogs;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.PubSubEvents;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;

namespace Presently.MobileApp.ViewModels
{
    public class ClockPageViewModel : ViewModelBase
    {
        public ClockPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            BackCommand = new DelegateCommand(async () => await PageNavigator.GoBackAsync());
        }

        public DelegateCommand BackCommand { get; private set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Publish(false);

            if (parameters.ContainsKey(NavParameters.LogType))
            {
                Title = parameters.GetValue<string>(NavParameters.LogType);
            }
        }
    }
}
