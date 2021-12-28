using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> RemoveCategory(string id);
        Task<bool> UpdateCategory(string id, CategoryDto category);
        Task<bool> AddCategory(CategoryDto category);
        Task<List<Category>> GetAllCategory();
        Task<List<Category>> GetCategories(string name);
        Task<Category> GetCategoryById(string id);
        Task<Category> GetCategoryByName(string name);

    }
}
