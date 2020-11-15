using Acr.UserDialogs;
using Presently.MobileApp.Models;
using Presently.MobileApp.PubSubEvents;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Events;
using Prism.Navigation;
using System.Threading.Tasks;

namespace Presently.MobileApp.ViewModels
{
    public class MasterDetailViewModelBase : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly SubscriptionToken _hamburgerTappedEventSubscriptionToken;
        private readonly SubscriptionToken _hamburgerNavigateEventSubscriptionToken;
        private readonly SubscriptionToken _hamburgerNavigateModalEventSubscriptionToken;
        private readonly SubscriptionToken _hamburgerNavigateBackToRootEventSubscriptionToken;
        private readonly SubscriptionToken _hamburgerSetSwipeGestureEventSubscriptionToken;

        public MasterDetailViewModelBase(
            INavigationService navigationService,
            IPageNavigator pageNavigator,
            IUserDialogs userDialogs,
            IEventAggregator eventAggregator)
            : base(pageNavigator, null, userDialogs, null, eventAggregator)
        {
            _navigationService = navigationService;

            _hamburgerTappedEventSubscriptionToken = EventAggregator.GetEvent<HamburgerTappedEvent>().Subscribe(() => OnPresentToggle(true), ThreadOption.UIThread);
            _hamburgerNavigateEventSubscriptionToken = EventAggregator.GetEvent<HamburgerNavigateEvent>().Subscribe(async (model) => await OnHamburgerNavigate(model), ThreadOption.UIThread);
            _hamburgerNavigateModalEventSubscriptionToken = EventAggregator.GetEvent<HamburgerNavigateModalEvent>().Subscribe(async (model) => await OnHamburgerNavigateModal(model), ThreadOption.UIThread);
            _hamburgerNavigateBackToRootEventSubscriptionToken = EventAggregator.GetEvent<HamburgerNavigateBackToRootEvent>().Subscribe(async (defaultPage) => await OnHamburgerNavigateBackToRoot(defaultPage), ThreadOption.UIThread);
            _hamburgerSetSwipeGestureEventSubscriptionToken = EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Subscribe((val) => IsGestureEnabled = val, ThreadOption.UIThread);
        }

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        private bool _isGestureEnabled;
        public bool IsGestureEnabled
        {
            get => _isGestureEnabled;
            set => SetProperty(ref _isGestureEnabled, value);
        }

        private void OnPresentToggle(bool isPresented)
        {
            IsPresented = isPresented;
        }

        private async Task OnHamburgerNavigate(HamburgerNavigateModel model)
        {
            IsGestureEnabled = false;
            await _navigationService.NavigateAsync($"NavigationPage/{model.Path}", model.Parameters, useModalNavigation: false, animated: false);
        }

        private async Task OnHamburgerNavigateModal(HamburgerNavigateModel model)
        {
            IsGestureEnabled = false;
            await _navigationService.NavigateAsync($"{model.Path}", model.Parameters, useModalNavigation: true, animated: false);
        }

        private async Task OnHamburgerNavigateBackToRoot(string defaultPage)
        {
            IsGestureEnabled = true;
            await _navigationService.NavigateAsync($"NavigationPage/{defaultPage}", null, useModalNavigation: false, animated: false);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            IsGestureEnabled = true;
        }

        public override void Destroy()
        {
            base.Destroy();
            EventAggregator.GetEvent<HamburgerTappedEvent>().Unsubscribe(_hamburgerTappedEventSubscriptionToken);
            EventAggregator.GetEvent<HamburgerNavigateEvent>().Unsubscribe(_hamburgerNavigateEventSubscriptionToken);
            EventAggregator.GetEvent<HamburgerNavigateModalEvent>().Unsubscribe(_hamburgerNavigateModalEventSubscriptionToken);
            EventAggregator.GetEvent<HamburgerNavigateBackToRootEvent>().Unsubscribe(_hamburgerNavigateBackToRootEventSubscriptionToken);
            EventAggregator.GetEvent<HamburgerSetSwipeGestureEvent>().Unsubscribe(_hamburgerSetSwipeGestureEventSubscriptionToken);
        }
    }
}
