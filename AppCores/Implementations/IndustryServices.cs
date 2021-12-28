using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Implementations
{
    public class IndustryServices : IIndustryService
    {
        private readonly IIndustryRepository _industryRepo;

        public IndustryServices(IIndustryRepository industryRepository)
        {
            _industryRepo = industryRepository;
        }
        public Task<bool> AddIndustry(IndustryDto industry)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Industry>> GetAllIndustry()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Industry>> GetCategories(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Industry> GetIndustryById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Industry> GetIndustryByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveIndustry(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateIndustry(string id, IndustryDto industry)
        {
            throw new System.NotImplementedException();
        }
    }
}
