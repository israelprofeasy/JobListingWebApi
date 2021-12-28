using System.ComponentModel.DataAnnotations;

namespace JobWebApi.AppModels.DTOs
{
    public class IndustryDto
    {

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        public string Name { get; set; }
    }
}
