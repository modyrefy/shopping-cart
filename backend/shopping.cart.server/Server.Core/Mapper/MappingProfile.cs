using AutoMapper;
using Server.Model.Dto.Brand;
using Server.Model.Dto.User;
using Server.Model.Models;

namespace Server.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, Users>().ReverseMap();
            CreateMap<ActiveUserContext, Users>();
            CreateMap<UserModel,ActiveUserContext>().ReverseMap();
            CreateMap<Users,ActiveUserContext>()
                .ForMember(s=>s.UserRole,c=>c.MapFrom(m=>m.UserRole.UserRole))
                .ForMember(s => s.UserState, c => c.MapFrom(m => m.UserState.UserState))
                ;
            CreateMap<BrandModel,Brands>().ReverseMap()
                .ForMember(s=>s.CategoryNameEn,c=>c.MapFrom(m=>m.Category.NameEn))
                .ForMember(s => s.CategoryNameAr, c => c.MapFrom(m => m.Category.NameAr))
                ;
        }
    }
}
