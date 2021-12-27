using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JobWebApi.AppModels.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public CvUpload CvUpload { get; set; } = new CvUpload();
        public List<JobApplication> AppliedJobs { get; set; } = new List<JobApplication>();

    }
}
