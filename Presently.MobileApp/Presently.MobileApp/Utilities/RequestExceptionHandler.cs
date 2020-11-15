using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Localization;
using Presently.MobileApp.Utilities.Abstractions;
using System;
using System.Threading.Tasks;

namespace Presently.MobileApp.Utilities
{
    public class RequestExceptionHandler : IRequestExceptionHandler
    {
        public async Task HandlerRequestTaskAsync(Func<Task> task)
        {
            try
            {
                await task();
            }
            catch (NoInternetConnectivityException)
            {
                throw new NoInternetConnectivityException();
            }
            catch (AuthInvalidCredentialException)
            {
                throw;
            }
            catch (DomainException)
            {
                throw;
            }
            catch (NoContentException)
            {
                throw;
            }
            catch (InvalidAuthTokenException)
            {
                throw new InvalidAuthTokenException();
            }
            catch (ServerMessageException ex)
            {
                throw new DomainException(ex.Message);
            }
            catch (ServerErrorException ex)
            {
                throw new DomainException(AppResources.Error_ServerErrror, string.Empty, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new DomainException(AppResources.Error_ServerErrror, "", ex.InnerException);
            }
        }

        public async Task<TResponse> HandlerRequestTaskAsync<TResponse>(Func<Task<TResponse>> task)
        {
            try
            {
                return await task();
            }
            catch (NoInternetConnectivityException)
            {
                throw new NoInternetConnectivityException();
            }
            catch (AuthInvalidCredentialException)
            {
                throw;
            }
            catch (DomainException)
            {
                throw;
            }
            catch (NoContentException)
            {
                throw;
            }
            catch (InvalidAuthTokenException)
            {
                throw new InvalidAuthTokenException();
            }
            catch (ServerMessageException ex)
            {
                throw new DomainException(ex.Message);
            }
            catch (ServerErrorException ex)
            {
                throw new DomainException(AppResources.Error_ServerErrror, string.Empty, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new DomainException(AppResources.Error_ServerErrror, string.Empty, ex.InnerException);
            }
        }
    }
}
