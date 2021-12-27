using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Interfaces
{
    public interface ICVUpload : ICrudRepo
    {
        Task<CvUpload> GetUpload(string userId);
        Task<IEnumerable<CvUpload>> GetAllUploads();
    }
}
