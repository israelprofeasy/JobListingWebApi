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
            CreateMap<CategoryDto, Category>();
            CreateMap<IndustryDto, Industry>();
            CreateMap<Category, CategoryReturnDto>();
            CreateMap<Industry, IndustryReturnedDto>();
            CreateMap<Job,JobsAppliedDto>()
                .ForMember(x => x.Location, c => c.MapFrom(c => c.Location.ToString()))
                .ForMember(x => x.JobNature, c => c.MapFrom(c => c.JobNature.ToString()));
            CreateMap<Job, JobPreviewDto>()
                .ForMember(x => x.SalaryRange, c => c.MapFrom(c => $"₦{c.MinimumSalary} to ₦{c.MaximumSalary}"))
                .ForMember(x => x.Location, c => c.MapFrom(c => c.Location.ToString()))
                .ForMember(x => x.JobNature, c => c.MapFrom(c => c.JobNature.ToString()));
            CreateMap<Job, JobDetailReturnedDto>()
                .ForMember(x => x.Location, c => c.MapFrom(c => c.Location.ToString()))
                .ForMember(x => x.JobNature, c => c.MapFrom(c => c.JobNature.ToString()))
                .ForMember(x => x.SalaryRange, c => c.MapFrom(c => $"₦{c.MinimumSalary} to ₦{c.MaximumSalary}"));
            CreateMap<JobDetailDto, Job>();
            CreateMap<Job, JobsAppliedDto>();
            CreateMap<AppUser, UserAppliedDto>();
            CreateMap<AppUser, UserDetailReturnedDto>();
        }
    }
}
