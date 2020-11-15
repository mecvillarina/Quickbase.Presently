using Acr.UserDialogs;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Localization;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Models;
using Presently.MobileApp.PubSubEvents;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms.GoogleMaps;

namespace Presently.MobileApp.ViewModels
{
    public class ClockPageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;
        private readonly IGeolocation _geolocation;
        private readonly IAttendanceLogManager _attendanceLogManager;
        private readonly SubscriptionToken _mapDragPinNewLocationEventToken;

        public ClockPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            ILocationManager locationManager,
            IGeolocation geolocation,
            IAttendanceLogManager attendanceLogManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _locationManager = locationManager;
            _geolocation = geolocation;
            _attendanceLogManager = attendanceLogManager;

            BackCommand = new DelegateCommand(async () => await PageNavigator.GoBackAsync());
            SubmitCommand = new DelegateCommand(async () => await OnSubmit(), () => OnSubmitCanExecute()).ObservesProperty(() => CurrentPostion).ObservesProperty(() => CurrentLocationName);

            _mapDragPinNewLocationEventToken = EventAggregator.GetEvent<MapDragPinNewLocationEvent>().Subscribe(async (pos) => await SetPoint(pos.Latitude, pos.Longitude));
        }

        private Position CurrentPostion { get; set; }
        private string _logType = string.Empty;

        private bool _isSubmitCommandEnabled;
        public bool IsSubmitCommandEnabled
        {
            get => _isSubmitCommandEnabled;
            set => SetProperty(ref _isSubmitCommandEnabled, value);
        }

        private string _currentLocationName;
        public string CurrentLocationName
        {
            get => _currentLocationName;
            set => SetProperty(ref _currentLocationName, value);
        }
        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private bool OnSubmitCanExecute()
        {
            IsSubmitCommandEnabled = CurrentPostion.Latitude != 0 && CurrentPostion.Longitude != 0 && !string.IsNullOrEmpty(CurrentLocationName);
            return IsSubmitCommandEnabled;
        }

        private async Task OnSubmit()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingSubmitting);

                var req = new AttendanceLogCreateRequestEntity()
                {
                    LocationName = CurrentLocationName,
                    Latitude = CurrentPostion.Latitude,
                    Longitude = CurrentPostion.Longitude,
                    LogType = _logType
                };

                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _attendanceLogManager.Create(req));

                UserDialogs.Toast(string.Format(AppResources.LabelSuccessMessage, _logType.ToLower()));
                await PageNavigator.GoBackAsync();
            }
            catch (NoInternetConnectivityException)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_NoInternetConnectivity, string.Empty, AppResources.ButtonOk);
            }
            catch (DomainException ex)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(ex.Message, string.Empty, AppResources.ButtonOk);
            }
            catch (InvalidAuthTokenException)
            {
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_SessionExpireMessage, AppResources.Error_SessionExpireTitle);
                await PageNavigator.ForceLogout();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.AppCenterLogError(ex);
                UserDialogs.HideLoading();
                await UserDialogs.AlertAsync(AppResources.Error_DefaultServerError, string.Empty, AppResources.ButtonOk);
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        private async Task SetPoint(double latitude, double longitude)
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetchingSelectedLocationAddress);
                CurrentPostion = new Position(latitude, longitude);
                EventAggregator.GetEvent<MapSetPinEvent>().Publish(new MapSetPinModel(CurrentPostion.Latitude, CurrentPostion.Longitude, "icon_pin_blue.png", MapTags.CurrentLocation) { IsDraggable = true });
                CurrentLocationName = await FetchAddressName(CurrentPostion.Latitude, CurrentPostion.Longitude);
            }
            catch
            {
                UserDialogs.Toast(AppResources.Error_CantAcquireSelectedLocationLocationAddress);
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        private async Task FetchCurrentLocation()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetchingCurrentLocation);
                int attempt = 0;

                var lastKnownLocation = await _geolocation.GetLastKnownLocationAsync();

                if (lastKnownLocation != null)
                {
                    CurrentPostion = new Position(lastKnownLocation.Latitude, lastKnownLocation.Longitude);
                    EventAggregator.GetEvent<MapSetPinEvent>().Publish(new MapSetPinModel(lastKnownLocation.Latitude, lastKnownLocation.Longitude, "icon_pin_blue.png", MapTags.CurrentLocation) { IsDraggable = true });
                    EventAggregator.GetEvent<MapMoveToRegionEvent>().Publish(new MapMoveToRegionModel(lastKnownLocation.Latitude, lastKnownLocation.Longitude, 15));
                    CurrentLocationName = await FetchAddressName(lastKnownLocation.Latitude, lastKnownLocation.Longitude);
                }

                do
                {
                    attempt++;
                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    var location = await _geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        CurrentPostion = new Position(location.Latitude, location.Longitude);
                        EventAggregator.GetEvent<MapSetPinEvent>().Publish(new MapSetPinModel(location.Latitude, location.Longitude, "icon_pin_blue.png", MapTags.CurrentLocation) { IsDraggable = true });
                        EventAggregator.GetEvent<MapMoveToRegionEvent>().Publish(new MapMoveToRegionModel(location.Latitude, location.Longitude, 15));
                        CurrentLocationName = await FetchAddressName(location.Latitude, location.Longitude);
                        UserDialogs.HideLoading();
                        return;
                    }
                } while (attempt < 3 && lastKnownLocation == null);

            }
            catch (Exception)
            {
                UserDialogs.Toast(AppResources.Error_CantAcquireCurrentLocationAddress);
            }
            finally
            {
                OnSubmitCanExecute();
                UserDialogs.HideLoading();
            }
        }

        private async Task<string> FetchAddressName(double latitude, double longitude)
        {
            try
            {
                var formattedAddress = await RequestExceptionHandler.HandlerRequestTaskAsync(() => _locationManager.AcquireAddress(latitude, longitude));
                return formattedAddress;
            }
            catch (Exception)
            {

            }

            return string.Empty;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Publish(false);

            if (parameters.ContainsKey(NavParameters.LogType))
            {
                _logType = parameters.GetValue<string>(NavParameters.LogType);
                Title = _logType;
            }

            if (parameters.GetNavigationMode() == Prism.Navigation.NavigationMode.New)
            {
                await FetchCurrentLocation();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            EventAggregator.GetEvent<MapMoveToRegionEvent>().Unsubscribe(_mapDragPinNewLocationEventToken);
        }
    }
}
