using System;
using System.ComponentModel;

namespace RemoteEducation.Model
{
    public class UserDetails : EntityBase
    {
        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
    }
}
