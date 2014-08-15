using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RemoteEducation.Model
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
        /// Is active.
        /// </summary>
        public bool Active { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
