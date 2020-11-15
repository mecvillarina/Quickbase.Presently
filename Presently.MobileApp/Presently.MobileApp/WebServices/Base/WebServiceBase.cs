using MimeMapping;
using Presently.Common.Abstractions;
using Presently.Common.DataContracts.Responses;
using Presently.MobileApp.Common.Enums;
using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.WebServices.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Base
{
    public abstract class WebServiceBase
    {
        private readonly IAppHttpClient _appHttpClient;
        private readonly IHttpMessageHelper _httpMessageHelper;

        protected WebServiceBase(IHttpService httpService, IHttpMessageHelper httpMessagepHelper)
        {
            _httpMessageHelper = httpMessagepHelper;
            _appHttpClient = httpService.AppHttpClient;
        }

        protected async Task<TResult> GetAsync<TResult>(string requestUri, Dictionary<string, string> headers = null, string accessToken = null) where TResult : IJsonDataContract
        {
            using (var response = await InternalJsonRequestAsync(HttpRequestType.Get, requestUri, null, headers, accessToken))
                return await _httpMessageHelper.DecodeJsonResponseToObject<TResult>(response);
        }

        protected async Task GetAsync(string requestUri, Dictionary<string, string> headers = null, string accessToken = null)
        {
            using (await InternalJsonRequestAsync(HttpRequestType.Get, requestUri, null, headers, accessToken))
                await Task.CompletedTask;
        }

        protected async Task<Stream> GetStreamAsync(string requestUri, Dictionary<string, string> headers = null, string accessToken = null)
        {
            var response = await InternalRequestAsync(HttpRequestType.Get, requestUri, null, headers, accessToken);
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }

        protected async Task PostAsync(string requestUri, IJsonDataContract contract = null, Dictionary<string, string> headers = null, string accessToken = null)
        {
            using (await InternalJsonRequestAsync(HttpRequestType.Post, requestUri, contract, headers, accessToken))
                await Task.CompletedTask;
        }

        protected async Task<TResult> PostAsync<TResult>(string requestUri, IJsonDataContract contract = null, Dictionary<string, string> headers = null, string accessToken = null) where TResult : IJsonDataContract
        {
            using (var response = await InternalJsonRequestAsync(HttpRequestType.Post, requestUri, contract, headers, accessToken))
                return await _httpMessageHelper.DecodeJsonResponseToObject<TResult>(response);
        }

        protected async Task<TResult> PostFileAsync<TResult>(string requestUri, IJsonDataContract contract = null, Dictionary<string, string> headers = null, string accessToken = null) where TResult : IJsonDataContract
        {
            using (var response = await InternalJsonRequestAsync(HttpRequestType.Post, requestUri, contract, headers, accessToken))
                return await _httpMessageHelper.DecodeJsonResponseToObject<TResult>(response);
        }

        protected async Task<TResult> PostFileAsync<TResult>(string requestUri, Stream filestream, string filename, Dictionary<string, string> headers = null, string accessToken = null) where TResult : IJsonDataContract
        {
            using (var response = await InternalMultipartFormDataRequestAsync(HttpRequestType.Post, requestUri, filestream, filename, headers, accessToken))
            {
                return await _httpMessageHelper.DecodeJsonResponseToObject<TResult>(response);
            }
        }

        protected async Task PutAsync(string requestUri, IJsonDataContract contract = null, Dictionary<string, string> headers = null, string accessToken = null)
        {
            using (await InternalJsonRequestAsync(HttpRequestType.Put, requestUri, contract, headers, accessToken))
                await Task.CompletedTask;
        }

        protected async Task DeleteAsync(string requestUri, IJsonDataContract contract = null, Dictionary<string, string> headers = null, string accessToken = null)
        {
            using (await InternalJsonRequestAsync(HttpRequestType.Delete, requestUri, contract, headers, accessToken))
                await Task.CompletedTask;
        }

        private async Task<HttpResponseMessage> InternalMultipartFormDataRequestAsync(HttpRequestType httpRequestType, string requestUri, Stream fileStream, string filename, Dictionary<string, string> headers, string accessToken)
        {
            using (var formHttpContent = new MultipartFormDataContent())
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                formHttpContent.Add(new StreamContent(fileStream), "UploadedFile", filename);

                return await InternalRequestAsync(httpRequestType, requestUri, formHttpContent, headers, accessToken);
            }
        }

        private async Task<HttpResponseMessage> InternalJsonRequestAsync(HttpRequestType httpRequestType, string requestUri, IJsonDataContract contract, Dictionary<string, string> headers, string accessToken)
        {
            if (contract != null)
            {
                using (var jsonHttpContent = _httpMessageHelper.EncodeObjectToJsonHttpContent(contract))
                {
                    Debug.WriteLine("JSON: " + await jsonHttpContent.ReadAsStringAsync());
                    return await InternalRequestAsync(httpRequestType, requestUri, jsonHttpContent, headers, accessToken);
                }
            }
            else
            {
                return await InternalRequestAsync(httpRequestType, requestUri, null, headers, accessToken);
            }
        }

        private async Task<HttpResponseMessage> InternalRequestAsync(HttpRequestType httpRequestType, string requestUri, HttpContent httpContent, Dictionary<string, string> headers, string accessToken)
        {
            _appHttpClient.DefaultRequestHeaders.Clear();
            _appHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(KnownMimeTypes.Json));

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _appHttpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            if (!string.IsNullOrEmpty(accessToken))
                _appHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _appHttpClient.RequestAsync(httpRequestType, requestUri, httpContent);

            var statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                if (statusCode == 204)
                {
                    throw new NoContentException("No content available.");
                }

                return response;
            }
            else
            {
                if (statusCode == 400)
                {
                    var badRequestContract = await _httpMessageHelper.DecodeJsonResponseToObject<BadRequestResponseContract>(response);

                    if (badRequestContract != null && !string.IsNullOrEmpty(badRequestContract.Message))
                    {
                        throw new ServerMessageException(badRequestContract.Message);
                    }
                }

                var method = httpRequestType.ToString().ToUpper();
                if (statusCode >= 500)
                {
                    throw new ServerErrorException("Server is unavailable.", new ApiException($"{method} request failed for endpoint {requestUri}.", method, statusCode, response.ReasonPhrase));
                }
                else
                {
                    var errorData = await _httpMessageHelper.DecodeJsonResponseToObject<Dictionary<string, object>>(response);
                    throw new ApiException($"{method} request failed.", method, statusCode, response.ReasonPhrase, new ApiErrorData(errorData));
                }
            }
        }
    }
}