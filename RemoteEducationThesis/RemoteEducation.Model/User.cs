using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class User : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier,
        /// </summary>
        [Index(IsUnique = true)]
        public Guid? Identifier { get; set; }

        /// <summary>
        /// Gets or sets user details ID.
        /// </summary>
        [ForeignKey("UserDetail")]
        public int UserDetailsID { get; set; }

        /// <summary>
        /// Gets or sets the user detail.
        /// </summary>
        public UserDetails UserDetail { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the security code.
        /// </summary>
        public int SecurityCode { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}
