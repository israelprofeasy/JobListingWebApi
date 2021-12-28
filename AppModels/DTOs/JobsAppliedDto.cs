using JobWebApi.AppModels.Enums;

namespace JobWebApi.AppModels.DTOs
{
    public class JobsAppliedDto
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public Locations Location { get; set; }

        
        public JobNature JobNature { get; set; }
    }
}
