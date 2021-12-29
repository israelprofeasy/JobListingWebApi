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
        Task<CategoryReturnDto> AddCategory(CategoryDto category);
        Task<List<CategoryReturnDto>> GetAllCategory();
        Task<List<CategoryReturnDto>> GetCategories(string name);
        Task<CategoryReturnDto> GetCategoryById(string id);
        Task<CategoryReturnDto> GetCategoryByName(string name);

    }
}
