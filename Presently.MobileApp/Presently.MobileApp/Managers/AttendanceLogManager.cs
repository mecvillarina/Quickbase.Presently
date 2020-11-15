using Newtonsoft.Json;
using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Base;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.DataObjects;
using Presently.MobileApp.WebServices.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers
{
    public class AttendanceLogManager : AuthenticatedManagerBase, IAttendanceLogManager
    {
        private readonly IAttendanceLogWebService _attendanceLogWebService;
        private readonly IAttendanceLogRepository _attendanceLogRepository;

        public AttendanceLogManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager authManager,
            IAttendanceLogWebService attendanceLogWebService,
            IAttendanceLogRepository attendanceLogRepository) : base(connectivity, mapper, authManager)
        {
            _attendanceLogWebService = attendanceLogWebService;
            _attendanceLogRepository = attendanceLogRepository;
        }

        public async Task<List<AttendanceLogEntity>> GetLogs()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _attendanceLogWebService.GetAll(accessToken);
                var dataObject = Mapper.Map<List<AttendanceLogDataObject>>(contract.ToList());
                _attendanceLogRepository.Clear();
                _attendanceLogRepository.AddRange(dataObject);

                return Mapper.Map<List<AttendanceLogEntity>>(contract.ToList());
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public List<AttendanceLogEntity> GetLogsLocally()
        {
            var dataObject = _attendanceLogRepository.ToList();
            return Mapper.Map<List<AttendanceLogEntity>>(dataObject);
        }

        public async Task<AttendanceLogEntity> Get(long recordId)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _attendanceLogWebService.GetSingle(recordId, accessToken);
                return Mapper.Map<AttendanceLogEntity>(contract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }


        public async Task<AttendanceLogEntity> Create(AttendanceLogCreateRequestEntity reqEntity)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var reqContract = Mapper.Map<AttendanceLogCreateRequestContract>(reqEntity);
                var accessToken = await GetAccessToken();

                var json = JsonConvert.SerializeObject(reqContract);
                var contract = await _attendanceLogWebService.Create(reqContract, accessToken);
                return Mapper.Map<AttendanceLogEntity>(contract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }
    }
}
