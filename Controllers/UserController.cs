using AutoMapper;
using JobWebApi.AppCommons;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(RegisterDto model)
        {

            var existingEmailUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmailUser != null)
            {
                ModelState.AddModelError("Invalid", $"User with email: {model.Email} already exists");
                return BadRequest(Utilities.BuildResponse<object>(false, "User already exists!", ModelState, null));
            }


            var user = _mapper.Map<AppUser>(model);



            var response = await _userManager.CreateAsync(user, model.Password);

            if (!response.Succeeded)
            {
                foreach (var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add user!", ModelState, null));
            }

            var res = await _userManager.AddToRoleAsync(user, "Regular");

            if (!res.Succeeded)
            {
                foreach (var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add user role!", ModelState, null));
            }


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfrimEmail", "User", new { Email = user.Email, Token = token }, Request.Scheme);


            var details = _mapper.Map<RegisterSuccessDto>(user);


            return Ok(Utilities.BuildResponse(true, "New user added!", null, new { details, ConfimationLink = url }));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfrimEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("Invalid", "UserId and token is required");
                return BadRequest(Utilities.BuildResponse<object>(false, "UserId or token is empty!", ModelState, null));
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("NotFound", $"User with email: {email} was not found");
                return NotFound(Utilities.BuildResponse<object>(false, "User not found!", ModelState, null));
            }

            var res = await _userManager.ConfirmEmailAsync(user, token);
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Utilities.BuildResponse<object>(false, "Failed to confirm email", ModelState, null));
            }

            return Ok(Utilities.BuildResponse<object>(true, "Email confirmation suceeded!", null, null));
        }

        [HttpPut("edit-user")]
        public IActionResult EditUser()
        {
            return Ok();
        }
        [HttpDelete("delete-user")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers(int page, int perPage)
        {
            // map data from db to dto to reshape it and remove null fields
            var listOfUsersToReturn = new List<UserToReturnDto>();

            //var users = _userService.Users;
            var users = _userManager.Users.ToList();

            if (users != null)
            {
                var pagedList = PageList<AppUser>.Paginate(users, page, perPage);
                foreach (var user in pagedList.Data)
                {
                    listOfUsersToReturn.Add(_mapper.Map<UserToReturnDto>(user));
                }

                var res = new PaginatedListDto<UserToReturnDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsersToReturn
                };

                return Ok(Utilities.BuildResponse(true, "List of users", null, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for users found!");
                var res = Utilities.BuildResponse<List<UserToReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }

        }
        [HttpGet("get-user-byId")]
        public IActionResult GetUserById()
        {
            return Ok();
        }
        [HttpGet("get-users-byname")]
        public IActionResult GetUsersByName()
        {
            return Ok();
        }
        [HttpGet("get-user-byemail")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {

            // map data from db to dto to reshape it and remove null fields
            var UserToReturn = new UserToReturnDto();
            //var user = await _userService.GetUser(email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                UserToReturn = new UserToReturnDto
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email
                };

                var res = Utilities.BuildResponse(true, "User details", null, UserToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with email {user.Email}");
                return NotFound(Utilities.BuildResponse<List<UserToReturnDto>>(false, "No result found!", ModelState, null));
            }

        }
        [HttpPut("deactivate-user")]
        public IActionResult DeactivateUser()
        {
            return Ok();
        }
        [HttpPut("activate-user")]
        public IActionResult ActivateUser()
        {
            return Ok();
        }
        [HttpGet("request-deactivation")]
        public IActionResult RequestDeactivation()
        {
            return Ok();
        }
    }
}
