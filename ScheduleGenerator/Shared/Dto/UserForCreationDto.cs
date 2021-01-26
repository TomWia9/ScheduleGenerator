using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    public class UserForCreationDto
    {
        [Required(AllowEmptyStrings = false)]
        [Range(3, 40, ErrorMessage = "The Email field must be between {1} and {2}.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(5, 20, ErrorMessage = "The Password field must be between {1} and {2}.")]
        public string Password { get; set; }
        
        [DisplayName("Confirm password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Confirm Password field is required.")]
        [Range(5, 20, ErrorMessage = "The Confirm Password field must be between {1} and {2}.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
