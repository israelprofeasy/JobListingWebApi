using AutoMapper;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;

namespace JobWebApi.AppCommons
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, AppUser>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(u => u.Email));
            CreateMap<AppUser, RegisterSuccessDto>()
               .ForMember(dest => dest.UserId, x => x.MapFrom(x => x.Id))
               .ForMember(d => d.FullName, x => x.MapFrom(x => $"{x.FirstName} {x.LastName}"));

            CreateMap<AppUser, UserToReturnDto>();
            CreateMap<UploadDto, CvUpload>();
            CreateMap<CvUpload, CvUploadReturnedDto>();
        }
    }
}
