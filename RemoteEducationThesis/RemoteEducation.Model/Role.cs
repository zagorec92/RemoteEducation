using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Education.Model
{
    public class Role : EntityBase
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
