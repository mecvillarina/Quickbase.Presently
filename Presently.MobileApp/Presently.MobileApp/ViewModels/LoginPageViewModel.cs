using Acr.UserDialogs;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Localization;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace Presently.MobileApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAuthManager _authManager;
        private readonly IAppUserManager _appUserManager;

        public LoginPageViewModel(IPageNavigator pageNavigator,
            ILogger logger,
            IUserDialogs userDialogs,
            IRequestExceptionHandler requestExceptionHandler,
            IAuthManager authManager,
            IAppUserManager appUserManager) : base(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
            _authManager = authManager;
            _appUserManager = appUserManager;

            LoginCommand = new DelegateCommand(async () => await OnLogin(), () => OnLoginCanExecute())
                .ObservesProperty(() => EmployeeId)
                .ObservesProperty(() => AccessCode);
        }

        private string _employeeId = string.Empty;
        public string EmployeeId
        {
            get => _employeeId;
            set => SetProperty(ref _employeeId, value);
        }

        private string _accessCode = string.Empty;
        public string AccessCode
        {
            get => _accessCode;
            set => SetProperty(ref _accessCode, value);
        }

        public DelegateCommand LoginCommand { get; private set; }

        public bool OnLoginCanExecute()
        {
            return !string.IsNullOrEmpty(EmployeeId) && !string.IsNullOrEmpty(AccessCode);
        }

        private async Task OnLogin()
        {
            try
            {
                UserDialogs.ShowLoading(AppResources.LoadingLoggingIn);

                var req = new AuthLoginRequestEntity()
                {
                    EmployeeId = EmployeeId,
                    AccessCode = AccessCode
                };

                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _authManager.Login(req));
                await RequestExceptionHandler.HandlerRequestTaskAsync(() => _appUserManager.GetProfile());

                await PageNavigator.NavigateAsync($"../{ViewNames.GetMainMasterPage()}");
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                _authManager.ClearAuthData();
            }

#if DEBUG
            EmployeeId = "1000";
            AccessCode = "34827";
#endif
        }
    }
}
