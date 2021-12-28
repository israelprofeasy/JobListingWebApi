using AutoMapper;
using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Implementations
{
    public class JobServices : IJobServices
    {
        private readonly IJobRepository _jobRepo;
        private readonly IMapper _mapper;

        public JobServices(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepo = jobRepository;
            _mapper = mapper;
        }
        public Task<JobPreviewDto> AddJob(JobDetailDto job)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetAllJobs()
        {
            throw new System.NotImplementedException();
        }

        public Task<JobDetailReturnedDto> GetJobById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsByCategory(string categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsByIndustry(string industryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsByLocation(Locations location)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsByNature(JobNature nature)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveJob(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateJob(string id, JobDetailDto job)
        {
            throw new System.NotImplementedException();
        }
    }
}
