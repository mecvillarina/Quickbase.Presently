using AutoMapper;
using Presently.Common.DataContracts;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Repositories.DataObjects;
using Presently.MobileApp.WebServices.DataContracts;

namespace Presently.MobileApp.Managers.Mappers.Profiles
{
    public class DataContractDataObjectProfile : Profile
    {
        public DataContractDataObjectProfile()
        {
            CreateMap<EmployeeContract, AppUserDataObject>();

        }
    }
}
