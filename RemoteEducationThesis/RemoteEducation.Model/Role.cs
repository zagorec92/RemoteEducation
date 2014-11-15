using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Education.Model
{
    public class Role : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
