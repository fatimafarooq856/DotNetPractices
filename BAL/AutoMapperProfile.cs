using AutoMapper;
using BAL.Model;
using DAL.Entities;
using NodaTime;

namespace BAL
{
    //When the application starts it will initialize AutoMapper and then AutoMapper scans all assemblies & look for classes
    //that inherit from the Profile class and load their mapping configurations. 
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LocalDate, DateTime>();
            CreateMap<DateTime, LocalDate>();
            CreateMap<UserDto, Student>()
               .ForMember(x => x.NIC, opt => opt.Ignore());
            //.ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id.Encode()));

            CreateMap<UserDto, User>();
            //.ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id.Encode()));
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore());

        }
    }
}
