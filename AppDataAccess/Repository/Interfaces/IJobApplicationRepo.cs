using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Interfaces
{
    public interface IJobApplicationRepo : ICrudRepo
    {
        Task<IEnumerable<AppUser>> JobApplications(string JobId);
        Task<IEnumerable<Job>> UserApplications(string userId);
    }
}
