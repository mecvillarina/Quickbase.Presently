using Presently.Common.Abstractions;
using Presently.Common.DataContracts;
using System.Collections.Generic;

namespace Presently.MobileApp.WebServices.DataContracts
{
    public class AttendanceLogCollectionResponseDataContract : List<AttendanceLogContract>, IJsonDataContract
    {
    }
}
