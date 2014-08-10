using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RemoteEducation.Model
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateModified { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
