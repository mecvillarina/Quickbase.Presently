using AutoMapper;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Repositories.DataObjects;

namespace Presently.MobileApp.Managers.Mappers.Profiles
{
    public class EntityDataObjectProfile : Profile
    {
        public EntityDataObjectProfile()
        {
            CreateMap<AppUserDataObject, AppUserEntity>();
            CreateMap<EmployeeSiteDataObject, EmployeeSiteEntity>();
            CreateMap<AttendanceLogDataObject, AttendanceLogEntity>();
        }
    }
}
