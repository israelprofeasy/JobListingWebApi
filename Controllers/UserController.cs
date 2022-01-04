using AutoMapper;
using CloudinaryDotNet.Actions;
using JobWebApi.AppCommons;
using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppModels.DTOs;
using JobWebApi.AppModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobWebApi.Controllers
{

    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, IMapper mapper, IUploadService uploadService)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _uploadService = uploadService;
        }
        [HttpPost("add-user")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(RegisterDto model)
        {

            var existingEmailUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmailUser != null)
            {
                ModelState.AddModelError("Invalid", $"User with email: {model.Email} already exists");
                return BadRequest(Utilities.BuildResponse<object>(false, "User already exists!", ModelState, null));
            }
            var user = _mapper.Map<AppUser>(model);
            user.IsActive = true;
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
        [AllowAnonymous]
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
        public async Task<IActionResult> EditUser(string id, UserToUpdateDto model)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!id.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", "You are not allowed to edit another user's details");
                var result2 = Utilities.BuildResponse<List<UserToUpdateDto>>(false, "Access denied!", ModelState, null);
                return BadRequest(result2);
            }
            var user = _mapper.Map<AppUser>(model);
            user.IsActive = true;
            var response = await _userManager.UpdateAsync(user);
            if (response != null)
            {

                var result = Utilities.BuildResponse(true, "User updated sucessfully!", null, response);
                return Ok(result);
            }

            ModelState.AddModelError("Failed", "User not updated");
            var res = Utilities.BuildResponse<List<UserToReturnDto>>(false, "Could not update details of user!", ModelState, null);
            return BadRequest(res);

        }
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (id.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", "You are not allowed to delete your account");
                var result2 = Utilities.BuildResponse<string>(false, "Access denied!", ModelState, null);
                return BadRequest(result2);
            }
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var res = Utilities.BuildResponse(true, "User has been successfully deleted!", null, result);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "User does not exist");
                var res = Utilities.BuildResponse<object>(false, "Invalid User Id", ModelState, null);
                return BadRequest(res);
            }
        }
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers(int page, int perPage)
        {
            // map data from db to dto to reshape it and remove null fields
            var listOfUsersToReturn = new List<UserToReturnDto>();

            //var users = _userService.Users;
            var users =await _userManager.Users.ToListAsync();

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
        [HttpGet("get-active-users")]
        public async Task<IActionResult> GetActiveUsers(int page, int perPage)
        {
            // map data from db to dto to reshape it and remove null fields
            var listOfUsersToReturn = new List<UserToReturnDto>();

            //var users = _userService.Users;
            var users = await _userManager.Users.ToListAsync();

            if (users != null)
            {
                var pagedList = PageList<AppUser>.Paginate(users, page, perPage);
                foreach (var user in pagedList.Data)
                {
                    if(user.IsActive == true)
                        listOfUsersToReturn.Add(_mapper.Map<UserToReturnDto>(user));
                }

                var res = new PaginatedListDto<UserToReturnDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsersToReturn
                };

                return Ok(Utilities.BuildResponse(true, "List of Active users", null, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for Active users found!");
                var res = Utilities.BuildResponse<List<UserToReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }

        }
        [HttpGet("get-user-byId")]
        public async Task<IActionResult> GetUserById(string id)
        {
            // map data from db to dto to reshape it and remove null fields
            var UserToReturn = new UserDetailReturnedDto();
            //var user = await _userService.GetUser(email);
            var user = await _userManager.Users.Where(x => x.Id == id).Include(x => x.CvUpload).FirstOrDefaultAsync();//FindByIdAsync(id);
            
            if (user != null)
            {
                UserToReturn = _mapper.Map<UserDetailReturnedDto>(user);

                var res = Utilities.BuildResponse(true, "User details", null, UserToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with id {user.Id}");
                return NotFound(Utilities.BuildResponse<List<UserToReturnDto>>(false, "No result found!", ModelState, null));
            }

        }
        [HttpGet("get-users-byname")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            // map data from db to dto to reshape it and remove null fields
            var UserToReturn = new List<UserToReturnDto>();
            //var user = await _userService.GetUser(email);
            var users =  await _userManager.Users.Where(x => x.FirstName == name || x.LastName == name).ToListAsync();
            if (users != null)
            {
                foreach (var user in users)
                {

                    UserToReturn.Add(_mapper.Map<UserToReturnDto>(user));
                }
                var res = Utilities.BuildResponse(true, "User details", null, UserToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with name {name}");
                return NotFound(Utilities.BuildResponse<List<UserToReturnDto>>(false, "No result found!", ModelState, null));
            }

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
                UserToReturn = _mapper.Map<UserToReturnDto>(user);

                var res = Utilities.BuildResponse(true, "User details", null, UserToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with email {email}");
                return NotFound(Utilities.BuildResponse<List<UserToReturnDto>>(false, "No result found!", ModelState, null));
            }

        }
        [HttpPut("deactivate-user")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = false;
            var res = await _userManager.UpdateAsync(user);
            if(res.Succeeded)
            {
                var response = Utilities.BuildResponse(true, "User has been successfully deactivated!", null, res);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "User does not exist");
                var response = Utilities.BuildResponse(false, "Invalid User Id", ModelState, res);
                return BadRequest(res);
            }
        }
        [HttpPut("activate-user")]
        public async Task<IActionResult> ActivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = true;
            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                var response = Utilities.BuildResponse(true, "User has been successfully activated!", null, res);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Failed", "User does not exist");
                var response = Utilities.BuildResponse(false, "Invalid User Id", ModelState, res);
                return BadRequest(res);
            }
        }
        [HttpPost("upload-cv")]
        public async Task<IActionResult> UploadCv([FromForm] UploadDto model, string userId)
        {
            //check if user logged is the one making the changes - only works for system using Auth tokens
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", $"You are not allowed to upload photo for another user");
                var result2 = Utilities.BuildResponse<string>(false, "Access denied!", ModelState, "");
                return BadRequest(result2);
            }
           // var ext = Path.GetExtension(model.Photo.FileName).Substring(1);
            if (model.Photo.FileName.Contains(".pdf"))
            {

                var file = model.Photo;

                if (file.Length > 0)
                {
                  
                    var uploadStatus = await _uploadService.UploadCvAsync(model, userId);

                    if (uploadStatus.Item1)
                    {
                        var res = await _uploadService.AddCvAsync(model, userId);
                        if (!res.Item1)
                        {
                            ModelState.AddModelError("Failed", "Could not add photo to database");
                            return BadRequest(Utilities.BuildResponse<ImageUploadResult>(false, "Failed to add to database", ModelState, null));
                        }

                        return Ok(Utilities.BuildResponse<object>(true, "Uploaded successfully", null, new { res.Item2.PublicId, res.Item2.Url }));
                    }

                    ModelState.AddModelError("Failed", "File could not be uploaded to cloudinary");
                    return BadRequest(Utilities.BuildResponse<ImageUploadResult>(false, "Failed to upload", ModelState, null));

                }

                ModelState.AddModelError("Invalid", "File size must not be empty");
                return BadRequest(Utilities.BuildResponse<ImageUploadResult>(false, "File is empty", ModelState, null));

            }
            ModelState.AddModelError("Invalid", "File size must be a .pdf file");
            return BadRequest(Utilities.BuildResponse<ImageUploadResult>(false, "File is not a pdf file", ModelState, null));
        }
        [HttpDelete("delete-upload-cv")]
        public async Task<IActionResult> DeleteUploadCv(string userId, string publicId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", $"You are not allowed to upload photo for another user");
                var result2 = Utilities.BuildResponse<string>(false, "Access denied!", ModelState, "");
                return BadRequest(result2);
            }
            var deleteCv = await _uploadService.DeletePhotoAsync(publicId);
            if (deleteCv)
            {
                return Ok(Utilities.BuildResponse(true, "Cv successfully deleted", null, ""));
            }
            ModelState.AddModelError("Invalid", "Incorrect public id");
            return BadRequest(Utilities.BuildResponse(false,"Insert the correct public Id",ModelState, ""));

        }
            [HttpGet("request-deactivation")]
        public IActionResult RequestDeactivation()
        {
            return Ok();
        }
    }
}
