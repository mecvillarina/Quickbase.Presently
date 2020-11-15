using Acr.UserDialogs;
using Presently.MobileApp.Utilities.Abstractions;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace Presently.MobileApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public IPageNavigator PageNavigator { get; private set; }
        public ILogger Logger { get; private set; }
        public IUserDialogs UserDialogs { get; private set; }
        public IRequestExceptionHandler RequestExceptionHandler { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public ViewModelBase(IPageNavigator pageNavigator)
        {
            PageNavigator = pageNavigator;
        }

        public ViewModelBase(IPageNavigator pageNavigator, ILogger logger) : this(pageNavigator)
        {
            Logger = logger;
        }

        public ViewModelBase(IPageNavigator pageNavigator, ILogger logger, IUserDialogs userDialogs) : this(pageNavigator, logger)
        {
            UserDialogs = userDialogs;
        }

        public ViewModelBase(IPageNavigator pageNavigator, ILogger logger, IUserDialogs userDialogs, IRequestExceptionHandler requestExceptionHandler) : this(pageNavigator, logger, userDialogs)
        {
            RequestExceptionHandler = requestExceptionHandler;
        }

        public ViewModelBase(IPageNavigator pageNavigator, ILogger logger, IUserDialogs userDialogs, IRequestExceptionHandler requestExceptionHandler, IEventAggregator eventAggregator) : this(pageNavigator, logger, userDialogs, requestExceptionHandler)
        {
            EventAggregator = eventAggregator;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
