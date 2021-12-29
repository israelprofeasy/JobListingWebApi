using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtService _jWTService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IJwtService jWTService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _jWTService = jWTService;
        }
        public async Task<LoginCredDto> Login(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = "";

            try
            {
               var res = await _signinManager.PasswordSignInAsync(user, password, rememberMe, false);

                if (!res.Succeeded)
                {
                    return new LoginCredDto { status = false };
                }

                // get jwt token
                var userRoles = await _userManager.GetRolesAsync(user);
                token = _jWTService.GenerateToken(user, userRoles.ToList());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new LoginCredDto { status = true, Id = user.Id, token = token };
        }
    }
}
