using Prism.Navigation;

namespace Presently.MobileApp.Models
{
    public class HamburgerNavigateModel
    {
        public string Path { get; set; }
        public INavigationParameters Parameters { get; set; } 
    }
}
