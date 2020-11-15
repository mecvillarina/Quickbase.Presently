using MimeMapping;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Utilities.Abstractions;
using Presently.MobileApp.WebServices.Abstractions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Presently.MobileApp.WebServices
{
    public class HttpService : IHttpService
    {
        private readonly IAppCenterLogger _appCenterLogger;
        private readonly IDebugLogger _debugLogger;

        public HttpService(IAppCenterLogger appCenterLogger, IDebugLogger debugLogger)
        {
            if (Cookie == null) Cookie = new CookieContainer();
            if (HttpClientHandler == null) HttpClientHandler = new HttpClientHandler { CookieContainer = Cookie, UseCookies = true };

            _appCenterLogger = appCenterLogger;
            _debugLogger = debugLogger;

            AppHttpClient = new AppHttpClient(HttpClientHandler, _appCenterLogger, _debugLogger) { BaseAddress = new Uri(Server.ApiBaseAddress) };
        }

        public IAppHttpClient AppHttpClient { get; private set; }

        public CookieContainer Cookie { get; }

        public HttpClientHandler HttpClientHandler { get; }

        public void ResetServerBaseAddress(string baseAddress)
        {
            AppHttpClient = new AppHttpClient(HttpClientHandler, _appCenterLogger, _debugLogger) { BaseAddress = new Uri(baseAddress) };
            AppHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(KnownMimeTypes.Json));
        }
    }
}
