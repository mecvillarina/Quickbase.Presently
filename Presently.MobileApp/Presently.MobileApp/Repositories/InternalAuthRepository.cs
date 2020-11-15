using Newtonsoft.Json;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.DataObjects;
using System;
using System.Threading.Tasks;

namespace Presently.MobileApp.Repositories
{
    public class InternalAuthRepository : IInternalAuthRepository
    {
        private readonly IKeyStoreRepository _keyStoreRepository;
        public InternalAuthRepository(IKeyStoreRepository keyStoreRepository)
        {
            _keyStoreRepository = keyStoreRepository;
        }

        private async Task<AuthDataObject> GetTokenPayload()
        {
            string tokenStr = await _keyStoreRepository.GetStringAsync(KeyStoreKeys.AuthToken);
            if (tokenStr != null)
            {
                return JsonConvert.DeserializeObject<AuthDataObject>(tokenStr);
            }

            return null;
        }

        public void ClearTokenPayload() => _keyStoreRepository.RemoveString(KeyStoreKeys.AuthToken);

        public async Task<string> GetToken()
        {
            var isTokenValid = await IsTokenValid();

            if (isTokenValid)
            {
                var tokenPayload = await GetTokenPayload();
                return tokenPayload.AccessToken;
            }

            return null;
        }
        public async Task<bool> IsTokenValid()
        {
            var currentToken = await GetTokenPayload();

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            if (currentToken != null)
                return DateTime.UtcNow < epoch.AddSeconds(currentToken.ExpireAt);
            else
                return false;
        }

        public async Task SetTokenPayload(AuthDataObject authDataObject)
        {
            string tokenStr = JsonConvert.SerializeObject(authDataObject);
            await _keyStoreRepository.SetStringAsync(KeyStoreKeys.AuthToken, tokenStr);
        }

      
    }
}
