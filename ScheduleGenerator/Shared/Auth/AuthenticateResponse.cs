namespace ScheduleGenerator.Shared.Auth
{
    /// <summary>
    /// User with Id, Email and Token fields
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The email of the user 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The token
        /// </summary>
        public string Token { get; set; }
    }
}
