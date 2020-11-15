using Presently.MobileApp.Common.Enums;
using Presently.MobileApp.Utilities.Abstractions;
using Presently.MobileApp.WebServices.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.WebServices
{
    public class AppHttpClient : IAppHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAppCenterLogger _appCenterLogger;
        private readonly IDebugLogger _debugLogger;

        public AppHttpClient(HttpClientHandler handler, IAppCenterLogger appCenterLogger, IDebugLogger debugLogger)
        {
            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _appCenterLogger = appCenterLogger;
            _debugLogger = debugLogger;
        }

        public Uri BaseAddress
        {
            get => _httpClient.BaseAddress;
            set => _httpClient.BaseAddress = value;
        }

        public HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

        public async Task<HttpResponseMessage> RequestAsync(HttpRequestType httpRequestType, string requestUri, HttpContent content = null)
        {
            HttpResponseMessage response;
            var method = httpRequestType.ToString().ToUpper();
            var httpTag = $"HTTP-{method}";
            var absoluteRequestUri = $"{BaseAddress}{requestUri}";

            try
            {
                _debugLogger.Write(httpTag, $"Sending {absoluteRequestUri}");

                switch (httpRequestType)
                {
                    case HttpRequestType.Delete:
                        response = await _httpClient.DeleteAsync(requestUri).ConfigureAwait(false);
                        break;

                    case HttpRequestType.Post:
                        response = await _httpClient.PostAsync(requestUri, content);
                        break;

                    case HttpRequestType.Put:
                        response = await _httpClient.PutAsync(requestUri, content).ConfigureAwait(false);
                        break;

                    case HttpRequestType.Get:
                        response = await _httpClient.GetAsync(requestUri).ConfigureAwait(false);
                        break;

                    default:
                        throw new NotSupportedException($"{httpRequestType} is not supported");
                }

                var statusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                    _debugLogger.Write(httpTag, $"Success ({statusCode} {response.ReasonPhrase}) {absoluteRequestUri}");
                else
                    _debugLogger.Write(httpTag, $"Failed ({statusCode} {response.ReasonPhrase}) {absoluteRequestUri}");

                return response;
            }
            catch (OperationCanceledException oex) when (oex.CancellationToken.IsCancellationRequested)
            {
                var timeoutException = new TimeoutException($"{httpTag} Request timed-out {absoluteRequestUri}");

                _appCenterLogger.LogError(timeoutException);

                _debugLogger.Write(httpTag, $"Request timed-out {absoluteRequestUri}");

                throw timeoutException;
            }
            catch (Exception ex)
            {
                _appCenterLogger.LogError(ex);

                _debugLogger.Write(httpTag, $"Error while sending the request {absoluteRequestUri}");

                throw;
            }
        }
    }
}