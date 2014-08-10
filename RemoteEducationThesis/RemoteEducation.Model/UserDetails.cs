using System;
using System.ComponentModel;

namespace RemoteEducation.Model
{
    public class UserDetails : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public DateTime DateModified { get; set; }
    }
}
