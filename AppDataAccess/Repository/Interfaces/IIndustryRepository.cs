using JobWebApi.AppModels.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppDataAccess.Repository.Interfaces
{
    public interface IIndustryRepository : ICrudRepo
    {
        Task<Industry> GetIndustryById(string id);
        Task<IEnumerable<Industry>> GetAll();
        Task<Industry> GetIndustryByName(string name);
        Task<IEnumerable<Industry>> GetIndustries(string name);
    }
}
