using AutoMapper;
using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppDataAccess.Repository.Interfaces;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepo = categoryRepository;
            _mapper = mapper;
        }
        public Task<bool> AddCategory(CategoryDto category)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Category>> GetAllCategory()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Category>> GetCategories(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveCategory(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCategory(string id, CategoryDto category)
        {
            throw new System.NotImplementedException();
        }
    }
}
