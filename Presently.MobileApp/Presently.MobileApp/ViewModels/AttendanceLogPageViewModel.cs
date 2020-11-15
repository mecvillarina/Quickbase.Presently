using Acr.UserDialogs;
using Presently.MobileApp.Localization;
using Presently.MobileApp.PubSubEvents;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;

namespace Presently.MobileApp.ViewModels
{
    public class AttendanceLogPageViewModel : ViewModelBase
    {
        public AttendanceLogPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            Title = AppResources.TitleAttendanceLogs;

            TappedMenuCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerTappedEvent>().Publish());
        }

        public DelegateCommand TappedMenuCommand { get; private set; }

    }
}
