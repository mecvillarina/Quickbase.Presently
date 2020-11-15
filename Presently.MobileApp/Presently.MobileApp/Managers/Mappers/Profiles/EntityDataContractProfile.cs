using AutoMapper;
using Presently.Common.DataContracts;
using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.WebServices.DataContracts;

namespace Presently.MobileApp.Managers.Mappers.Profiles
{
    public class EntityDataContractProfile : Profile
    {
        public EntityDataContractProfile()
        {
            CreateMap<AuthLoginRequestEntity, AuthLoginRequestContract>();
            CreateMap<AuthTokenDataContract, AuthTokenEntity>();
            CreateMap<EmployeeContract, AppUserEntity>();
        }
    }
}
