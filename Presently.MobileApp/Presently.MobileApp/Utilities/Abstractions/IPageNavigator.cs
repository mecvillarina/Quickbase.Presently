using Prism.Navigation;
using System.Threading.Tasks;

namespace Presently.MobileApp.Utilities.Abstractions
{
    public interface IPageNavigator
    {
        Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters = null);
        Task<INavigationResult> NavigateModalAsync(string name, INavigationParameters parameters = null);
        Task<INavigationResult> GoBackAsync(INavigationParameters parameters = null, bool? useModalNavigation = null);
        Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters = null);
        Task ForceLogout();
    }
}