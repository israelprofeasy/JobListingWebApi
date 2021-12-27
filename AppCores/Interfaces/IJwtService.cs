using JobWebApi.AppModels.Models;
using System.Collections.Generic;

namespace JobWebApi.AppCores.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, List<string> userRoles);
    }
}
