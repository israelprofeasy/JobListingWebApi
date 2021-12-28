using JobWebApi.AppModels.Enums;
using System;

namespace JobWebApi.AppModels.DTOs
{
    public class JobPreviewDto
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }


        public string Company { get; set; }

       
        public Locations Location { get; set; }

       // public string JobNature { get; set; }
        public JobNature JobNature { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }


        public string SalaryRange { get; set; }
    }
}
