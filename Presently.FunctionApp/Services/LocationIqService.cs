using Microsoft.Extensions.Options;
using Presently.Common.Models;
using Presently.FunctionApp.Services.Abstractions;
using RestSharp;

namespace Presently.FunctionApp.Services
{
    public class LocationIqService : ServiceBase, ILocationIqService
    {
        public LocationIqService(IOptions<AppSettings> options) : base(options)
        {
        }

        public string GetFormattedAddress(double latitude, double longitude)
        {
            do
            {
                try
                {
                    var client = new RestClient("https://us1.locationiq.com/");
                    string endpoint = string.Format("v1/reverse.php?key={0}&lat={1}&lon={2}&format=json", AppSettings.LocationIqApiKey, latitude, longitude);
                    var request = new RestRequest(endpoint, DataFormat.Json);
                    var response = client.Get<LocationIqBaseModel>(request);

                    return response.Data.DisplayName;
                }
                catch { }
            } while (true);
        }
    }
}
