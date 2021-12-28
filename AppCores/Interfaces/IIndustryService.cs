using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Interfaces
{
    public interface IIndustryService
    {
        Task<bool> RemoveIndustry(string id);
        Task<bool> UpdateIndustry(string id, IndustryDto industry);
        Task<bool> AddIndustry(IndustryDto industry);
        Task<List<Industry>> GetAllIndustry();
        Task<List<Industry>> GetCategories(string name);
        Task<Industry> GetIndustryById(string id);
        Task<Industry> GetIndustryByName(string name);
    }
}
