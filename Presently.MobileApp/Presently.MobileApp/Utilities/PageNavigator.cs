using Presently.MobileApp.Managers.Abstractions;
using Prism.Events;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Presently.MobileApp.Utilities
{
    public class PageNavigator
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IInternalAuthManager _authManager;

        public PageNavigator(INavigationService navigationService, IEventAggregator eventAggregator, IInternalAuthManager authManager)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _authManager = authManager;
        }

        public async Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters = null)
        {
            if (Device.IsInvokeRequired)
                return await Device.InvokeOnMainThreadAsync(() => _navigationService.NavigateAsync(name, parameters, false, false));

            return await _navigationService.NavigateAsync(name, parameters, false, false);
        }

        public async Task<INavigationResult> NavigateModalAsync(string name, INavigationParameters parameters = null)
        {
            if (Device.IsInvokeRequired)
                return await Device.InvokeOnMainThreadAsync(() => _navigationService.NavigateAsync(name, parameters, useModalNavigation: true, animated: false));

            return await _navigationService.NavigateAsync(name, parameters, useModalNavigation: true, animated: false);
        }

        public async Task<INavigationResult> GoBackAsync(INavigationParameters parameters = null, bool? useModalNavigation = null)
        {
            if (Device.IsInvokeRequired)
                return await Device.InvokeOnMainThreadAsync(() => _navigationService.GoBackAsync(parameters, useModalNavigation, animated: false));

            return await _navigationService.GoBackAsync(parameters, useModalNavigation, animated: false);
        }

        public async Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters = null)
        {
            if (Device.IsInvokeRequired)
                return await Device.InvokeOnMainThreadAsync(() => _navigationService.GoBackToRootAsync(parameters));

            return await _navigationService.GoBackToRootAsync(parameters);
        }
    }
}
