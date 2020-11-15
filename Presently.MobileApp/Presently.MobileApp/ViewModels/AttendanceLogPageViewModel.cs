using Acr.UserDialogs;
using Presently.MobileApp.Localization;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.PubSubEvents;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Presently.MobileApp.ViewModels
{
    public class AttendanceLogPageViewModel : ViewModelBase
    {
        private readonly IAttendanceLogManager _attendanceLogManager;

        public AttendanceLogPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            IAttendanceLogManager attendanceLogManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _attendanceLogManager = attendanceLogManager;
            Title = AppResources.TitleAttendanceLogs;

            TappedMenuCommand = new DelegateCommand(() => EventAggregator.GetEvent<HamburgerTappedEvent>().Publish());
            Logs = new ObservableCollection<AttendanceLogEntity>();
        }

        private ObservableCollection<AttendanceLogEntity> _logs;
        public ObservableCollection<AttendanceLogEntity> Logs
        {
            get => _logs;
            set => SetProperty(ref _logs, value);
        }

        public DelegateCommand TappedMenuCommand { get; private set; }

        private async Task FetchData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                var logs = await RequestExceptionHandler.HandlerRequestTaskAsync(() => _attendanceLogManager.GetLogs());
                Logs.Clear();

                foreach (var log in logs)
                {
                    Logs.Add(log);
                }
            }
            catch{ }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await Task.Delay(1000);
            await FetchData();
        }
    }
}
