using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Presently.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Presently.FunctionApp.Services
{
    public class ServiceBase
    {
        public readonly AppSettings AppSettings;

        public ServiceBase(IOptions<AppSettings> optionAppSettings)
        {
            AppSettings = optionAppSettings.Value;
        }

        public Dictionary<string, object> GetRequest(string path, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSettings.QuickbaseApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                headers ??= new Dictionary<string, string>();

                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                client.DefaultRequestHeaders.Add("QB-Realm-Hostname", AppSettings.QuickbaseRealmHostname);
                client.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {AppSettings.QuickbaseUserToken}");

                var response = client.GetAsync(path).Result;
                response.EnsureSuccessStatusCode();

                string content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            }
        }

        public K PostRequest<T, K>(string path, T payload, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSettings.QuickbaseApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                headers ??= new Dictionary<string, string>();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                client.DefaultRequestHeaders.Add("QB-Realm-Hostname", AppSettings.QuickbaseRealmHostname);
                client.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {AppSettings.QuickbaseUserToken}");

                var response = client.PostAsJsonAsync(path, payload).Result;
                response.EnsureSuccessStatusCode();

                string content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<K>(content);
            }
        }
    }
}
