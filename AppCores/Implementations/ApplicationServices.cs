using AutoMapper;
using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Implementations
{
    public class ApplicationServices : IApplication
    {
        private readonly IJobApplicationRepo _jobApplicationRepo;
        private readonly IMapper _mapper;

        public ApplicationServices(IJobApplicationRepo jobApplicationRepo, IMapper mapper)
        {
            _jobApplicationRepo = jobApplicationRepo;
            _mapper = mapper;
        }
        public Task<bool> ApplyForJob(string userId, string jobId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserAppliedDto>> JobApplications(string jobId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<JobsAppliedDto>> UserApplications(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
