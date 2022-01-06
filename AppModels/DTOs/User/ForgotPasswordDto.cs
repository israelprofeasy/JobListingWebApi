using System.ComponentModel.DataAnnotations;

namespace JobWebApi.AppModels.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
