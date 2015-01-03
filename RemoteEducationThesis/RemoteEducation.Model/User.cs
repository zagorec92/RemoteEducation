using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class User : EntityBase
    {
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
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the first and last name combined.
        /// </summary>
        public string FullName 
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}
