using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Managers.Abstractions;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers.Base
{
    public abstract class ManagerBase
    {
        private readonly IConnectivity _connectivity;
        public IServiceMapper Mapper { get; }

        protected ManagerBase(IConnectivity connectivity, IServiceMapper mapper)
        {
            _connectivity = connectivity;
            Mapper = mapper;
        }

        public void EnsureInternetAvailable()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new NoInternetConnectivityException();
        }
    }
}
