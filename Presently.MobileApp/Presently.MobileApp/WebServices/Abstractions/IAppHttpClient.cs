using Presently.MobileApp.Common.Enums;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface IAppHttpClient
    {
        Uri BaseAddress { get; set; }

        HttpRequestHeaders DefaultRequestHeaders { get; }

        Task<HttpResponseMessage> RequestAsync(HttpRequestType httpRequestType, string requestUri, HttpContent content = null);
    }
}
