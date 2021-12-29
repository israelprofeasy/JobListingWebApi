using JobWebApi.AppCommons;
using JobWebApi.AppCores.Interfaces;
using JobWebApi.AppModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplication _application;

        public ApplicationController(IApplication application)
        {
            _application = application;
        }
        [HttpPost("apply-job")]
        public async Task<IActionResult> ApplyForJob(string userId, string jobId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(currentUserId))
            {
                ModelState.AddModelError("Denied", $"You are not allowed to apply job for another user");
                var result2 = Utilities.BuildResponse<string>(false, "Access denied!", ModelState, "");
                return BadRequest(result2);
            }
            var apply = await _application.ApplyForJob(userId, jobId);
            if (apply != null)
            {
                if(apply.Success)
                return Ok(Utilities.BuildResponse(true, "Application Succesful", null,apply));
                else
                    return BadRequest(Utilities.BuildResponse(false, "Application Unsuccesful", null, apply));
            }
            return BadRequest();
        }
        [HttpGet("jobs-applied-for")]
        public async Task<IActionResult> GetJobsAppliedFor(string userId, int page, int perPage)
        {
            var jobs = await _application.UserApplications(userId);
            if (jobs != null)
            {
                var pagedList = PageList<JobsAppliedDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobsAppliedDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = pagedList.Data
                };

                return Ok(Utilities.BuildResponse(true, "List of Jobs Applied for", null, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for Jobs Applied for!");
                var res = Utilities.BuildResponse<List<JobsAppliedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("job-applications")]
        public async Task<IActionResult> GetJobApplications(string jobId, int page, int perPage)
        {
            var applicants = await _application.JobApplications(jobId);
            if (applicants != null)
            {
                var pagedList = PageList<UserAppliedDto>.Paginate(applicants, page, perPage);
                var res = new PaginatedListDto<UserAppliedDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = pagedList.Data
                };

                return Ok(Utilities.BuildResponse(true, "List of Jobs Applicant", null, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for Jobs Applicant for!");
                var res = Utilities.BuildResponse<List<JobsAppliedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
    }
}
