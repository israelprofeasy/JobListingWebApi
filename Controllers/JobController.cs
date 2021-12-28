using Microsoft.AspNetCore.Mvc;

namespace JobWebApi.Controllers
{
    public class JobController : ControllerBase
    {
        [HttpGet("GetJob/{name}")]
        public IActionResult GetJobsByName(string name)
        {

            return Ok();
        }
        [HttpGet("GetJob/{id}")]
        public IActionResult GetJobsById(string id)
        {

            return Ok();
        }

        [HttpGet("GetJobs")]
        public async Task<IActionResult> GetJobs()
        {
            var result = await _jobEFRepo.GetJobs();
            List<JobListingPreviewDto> jobs = new List<JobListingPreviewDto>();
            if (result != null)
            {
                foreach (var job in result)
                {

                    jobs.Add(_mapper.Map<JobListingPreviewDto>(job));
                }
                return Ok(Utilities.BuildResponse(true, "Job sucessfully added!", null, jobs));
            }
            else
            {

                ModelState.AddModelError("Failed", "Invalid Details");

                return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add Job!", ModelState, null));
            }

        }

        [HttpGet("GetJob/Category/{name}")]
        public IActionResult GetJobByCategory(string name)
        {

            return Ok();
        }

        [HttpGet("GetJob/Industry/{name}")]
        public IActionResult GetJobByIndustry(string name)
        {

            return Ok();
        }
        [HttpGet("GetJob/Location/{name}")]
        public IActionResult GetJobByLocation(string name)
        {

            return Ok();
        }
        [HttpGet("GetJob/SalaryRange/{name}")]
        public IActionResult GetJobBySalaryRange(int minimum, int maximum)
        {

            return Ok();
        }

        [HttpGet("GetJob/Nature/{name}")]
        public IActionResult GetJobByNature(string name)
        {

            return Ok();
        }
        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob(JobDetailDto model)
        {
            var job = _mapper.Map<Job>(model);
            var response = await _jobEFRepo.Add(job);
            if (response)
            {
                var details = _mapper.Map<JobListingPreviewDto>(job);
                return Ok(Utilities.BuildResponse(true, "Job sucessfully added!", null, details));
            }
            else
            {

                ModelState.AddModelError("Failed", "Invalid Details");

                return BadRequest(Utilities.BuildResponse<string>(false, "Failed to add Job!", ModelState, null));
            }

        }

        [HttpPost("AddCategory")]
        public IActionResult AddJobCategory(JobDetailDto model)
        {

            return Ok();
        }
        [HttpDelete("DeleteJob")]
        public IActionResult DeleteJob(string id)
        {

            return Ok();
        }
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(string id)
        {

            return Ok();
        }
        [HttpPut("UpdateJob/{name}")]
        public IActionResult EditJob(string id, JobDetailDto model)
        {

            return Ok();
        }
        [HttpPut("UpdateCategory/{name}")]
        public IActionResult EditCategory(string id, CategoryDto model)
        {


            return Ok();
        }
    }
}
