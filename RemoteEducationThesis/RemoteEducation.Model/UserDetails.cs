namespace Education.Model
{
    public class UserDetails : EntityBase
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}
