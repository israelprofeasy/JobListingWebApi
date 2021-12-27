using JobWebApi.AppModels.DTOs;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Interfaces
{
    public interface IAuthService
    {
        Task<LoginCredDto> Login(string email, string password, bool rememberMe);
    }
}
