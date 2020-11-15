using MimeMapping;
using Presently.MobileApp.WebServices.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Utilities
{
    public class HttpMessageHelper : IHttpMessageHelper
    {
        private readonly IJsonHelper _jsonHelper;

        public HttpMessageHelper(IJsonHelper jsonHelper)
        {
            _jsonHelper = jsonHelper;
        }

        public HttpContent EncodeObjectToJsonHttpContent<T>(T content)
        {
            if (content != null)
            {
                var ms = new MemoryStream();

                _jsonHelper.SerializeObjectToStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);

                var httpContent = new StreamContent(ms);
                httpContent.Headers.ContentEncoding.Add("utf-8");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(KnownMimeTypes.Json);

                return httpContent;
            }

            throw new ArgumentNullException(nameof(content));
        }

        public async Task<T> DecodeJsonResponseToObject<T>(HttpResponseMessage responseMessage)
        {
            using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                return _jsonHelper.DeserializeObjectFromStream<T>(contentStream);
        }
    }
}