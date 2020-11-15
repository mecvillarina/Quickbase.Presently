using AutoMapper;
using Presently.Common.DataContracts;
using Presently.MobileApp.Repositories.DataObjects;

namespace Presently.MobileApp.Managers.Mappers.Profiles
{
    public class DataContractDataObjectProfile : Profile
    {
        public DataContractDataObjectProfile()
        {
            CreateMap<EmployeeContract, AppUserDataObject>();
            CreateMap<AttendanceLogContract, AttendanceLogDataObject>();
            CreateMap<EmployeeSiteContract, EmployeeSiteDataObject>();
        }
    }
}
