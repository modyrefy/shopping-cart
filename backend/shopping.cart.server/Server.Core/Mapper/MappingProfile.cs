using AutoMapper;
using Server.Model.Dto.Brand;
using Server.Model.Dto.Category;
using Server.Model.Dto.Product;
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
            CreateMap<CategoryModel, Categories>().ReverseMap();
            CreateMap<ProductImagesModel, ProductImages>().ReverseMap();
            CreateMap<ProductTagsModel, ProductTags>().ReverseMap();
            CreateMap<CategoryModel, Categories>().ReverseMap();

            CreateMap<ProductModel, Products>()
                 .ForMember(s => s.ProductNameAr, c => c.MapFrom(m => m.NameAr))
                .ForMember(s => s.ProductNameEn, c => c.MapFrom(m => m.NameEn))
                .ForMember(s=>s.ProductImages,c=>c.MapFrom(m=>m.Images))
                .ForMember(s => s.ProductTags, c => c.MapFrom(m => m.Tags))
                .ReverseMap();

            CreateMap<Users,ActiveUserContext>()
                .ForMember(s=>s.UserRole,c=>c.MapFrom(m=>m.UserRole.UserRole))
                .ForMember(s => s.UserState, c => c.MapFrom(m => m.UserState.UserState));
            CreateMap<Brands, BrandModel>()
                .ForMember(s=>s.CategoryNameEn,c=>c.MapFrom(m=>m.Category.NameEn))
                .ForMember(s => s.CategoryNameAr, c => c.MapFrom(m => m.Category.NameAr))
                ;
            CreateMap<BrandModel, Brands>();
        }
    }
}
