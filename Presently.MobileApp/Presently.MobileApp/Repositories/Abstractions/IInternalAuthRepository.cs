using Presently.MobileApp.Repositories.DataObjects;
using System.Threading.Tasks;

namespace Presently.MobileApp.Repositories.Abstractions
{
    public interface IInternalAuthRepository
    {
        void ClearTokenPayload();
        Task<string> GetToken();
        Task<bool> IsTokenValid();
        Task SetTokenPayload(AuthDataObject authDataObject);
    }
}