using System.ComponentModel;

namespace ScheduleGenerator.Shared.Dto
{
    public class UserForCreationDto
    {
        /// <summary>
        /// The email of the user 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The password of the user 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The confirmation of user password
        /// </summary>
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
