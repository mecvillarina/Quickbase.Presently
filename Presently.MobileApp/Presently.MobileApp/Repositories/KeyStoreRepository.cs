using Presently.MobileApp.Repositories.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Presently.MobileApp.Repositories
{
    public class KeyStoreRepository : IKeyStoreRepository
    {
        public Task<string> GetStringAsync(string key) => SecureStorage.GetAsync(key);

        public void RemoveString(string key) => SecureStorage.Remove(key);

        public Task SetStringAsync(string key, string value) => SecureStorage.SetAsync(key, value);
    }
}
