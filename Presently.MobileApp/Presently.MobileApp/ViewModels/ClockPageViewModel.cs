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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Presently.MobileApp.ViewModels
{
    public class ClockPageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;
        private readonly IGeolocation _geolocation;
        private readonly IAppUserManager _appUserManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAttendanceLogManager _attendanceLogManager;
        private readonly SubscriptionToken _mapDragPinNewLocationEventToken;

        public ClockPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IEventAggregator eventAggregator,
            ILocationManager locationManager,
            IGeolocation geolocation,
            IAppUserManager appUserManager,
            IEmployeeManager employeeManager,
            IAttendanceLogManager attendanceLogManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler, eventAggregator)
        {
            _locationManager = locationManager;
            _geolocation = geolocation;
            _appUserManager = appUserManager;
            _employeeManager = employeeManager;
            _attendanceLogManager = attendanceLogManager;

            BackCommand = new DelegateCommand(async () => await PageNavigator.GoBackAsync());
            SubmitCommand = new DelegateCommand(async () => await OnSubmit(), () => OnSubmitCanExecute()).ObservesProperty(() => CurrentPostion).ObservesProperty(() => CurrentLocationName);

            _mapDragPinNewLocationEventToken = EventAggregator.GetEvent<MapDragPinNewLocationEvent>().Subscribe(async (pos) => await SetPoint(pos.Latitude, pos.Longitude));

            Geofences = new ObservableCollection<Circle>();
        }

        private Position CurrentPostion { get; set; }
        private string _logType = string.Empty;
        private bool _useGeofencing;
        private List<EmployeeSiteEntity> _sites;

        public ObservableCollection<Circle> Geofences { get; set; }

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
                bool isWithinGeofencing = false;

                if (_useGeofencing)
                {
                    foreach (var site in _sites)
                    {
                        if (VerifyDistance(new Position(site.SiteLatitude, site.SiteLongitude), CurrentPostion, site.SiteRadius))
                        {
                            isWithinGeofencing = true;
                            break;
                            
                        }
                    }

                    if (!isWithinGeofencing)
                    {
                        await UserDialogs.AlertAsync(AppResources.LabelVerifyLocationOutsideMessage);
                        return;
                    }
                }

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

        public void InitGeofences(List<EmployeeSiteEntity> sites)
        {
            if (sites != null && sites.Any())
            {
                Geofences.Clear();
                foreach (var site in sites)
                {
                    var circle = new Circle();
                    circle.StrokeWidth = 1f;
                    circle.StrokeColor = (Color)App.Current.Resources["PrimaryColor1Opaque"];
                    circle.FillColor = (Color)App.Current.Resources["PrimaryColor1Opaque"];
                    circle.Center = new Xamarin.Forms.GoogleMaps.Position(site.SiteLatitude, site.SiteLongitude);
                    circle.Radius = Distance.FromMeters(site.SiteRadius);
                    Geofences.Add(circle);
                }
            }

        }

        private async Task FetchData()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingFetching);
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _appUserManager.GetProfile());

                var profile = _appUserManager.GetProfileLocally();
                _useGeofencing = profile.UseGeofencing;

                if (_useGeofencing)
                {
                    _sites = await RequestExceptionHandler.HandlerRequestTaskAsync(() => _employeeManager.GetSites());
                    InitGeofences(_sites);
                }
            }
            catch
            {

            }
            finally
            {
                UserDialogs.HideLoading();
                await FetchCurrentLocation();
            }
        }

        private bool VerifyDistance(Position pos1, Position pos2, double distanceInMeter)
        {
            bool inDistance = false;

            var distance = GetDistance(pos1, pos2);
            if (distance <= distanceInMeter)
            {
                inDistance = true;
            }

            return inDistance;
        }

        private double GetDistance(Position pos1, Position pos2)
        {
            var R = 6371d; //Radius of the Earth in km
            var dLat = ConvertToRadians(pos2.Latitude - pos1.Latitude);
            var dLong = ConvertToRadians(pos2.Longitude - pos1.Longitude);

            var lat1 = ConvertToRadians(pos1.Latitude);
            var lat2 = ConvertToRadians(pos2.Latitude);

            var a = Math.Pow(Math.Sin(dLat / 2), 2)
                + Math.Pow(Math.Sin(dLong / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c * 1000;

            return d;

        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
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
                await FetchData();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            EventAggregator.GetEvent<MapMoveToRegionEvent>().Unsubscribe(_mapDragPinNewLocationEventToken);
        }
    }
}
