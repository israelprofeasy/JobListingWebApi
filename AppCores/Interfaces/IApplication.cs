using JobWebApi.AppModels.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Interfaces
{
    public interface IApplication
    {
        Task<bool> ApplyForJob(string userId, string jobId);
        Task<List<UserAppliedDto>> JobApplications(string jobId);
        Task<List<JobsAppliedDto>> UserApplications(string userId);

    }
}
