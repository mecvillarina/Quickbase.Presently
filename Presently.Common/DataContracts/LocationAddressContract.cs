using Presently.Common.Abstractions;

namespace Presently.Common.DataContracts
{
    public class LocationAddressContract : IJsonDataContract
    {
        public string FormattedTextAddress { get; set; }
    }
}
