﻿namespace ScheduleGenerator.Shared.Auth
{
    /// <summary>
    /// User's email and password
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// The email of the user 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user 
        /// </summary>
        public string Password { get; set; }
    }
}
