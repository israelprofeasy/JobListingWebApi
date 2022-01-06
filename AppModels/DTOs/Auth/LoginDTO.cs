using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebApi.AppModels.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool RememberMe { get; set; }

    }
}
