using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface ILocationManager
    {
        Task<string> AcquireAddress(double latitude, double longitude);
    }
}