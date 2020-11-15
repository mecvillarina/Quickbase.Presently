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
    public class EmployeeManager : AuthenticatedManagerBase, IEmployeeManager
    {
        private readonly IEmployeeWebService _employeeWebService;
        private readonly IEmployeeSiteRepository _employeeSiteRepository;
        public EmployeeManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager authManager,
            IEmployeeWebService employeeWebService,
            IEmployeeSiteRepository employeeSiteRepository) : base(connectivity, mapper, authManager)
        {
            _employeeWebService = employeeWebService;
            _employeeSiteRepository = employeeSiteRepository;
        }

        public async Task<List<EmployeeSiteEntity>> GetSites()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _employeeWebService.GetEmployeeSites(accessToken);
                var dataObject = Mapper.Map<List<EmployeeSiteDataObject>>(contract.ToList());
                _employeeSiteRepository.Clear();
                _employeeSiteRepository.AddRange(dataObject);

                return Mapper.Map<List<EmployeeSiteEntity>>(contract.ToList());
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public List<EmployeeSiteEntity> GetSitesLocally()
        {
            var dataObject = _employeeSiteRepository.ToList();
            return Mapper.Map<List<EmployeeSiteEntity>>(dataObject);
        }
    }
}
