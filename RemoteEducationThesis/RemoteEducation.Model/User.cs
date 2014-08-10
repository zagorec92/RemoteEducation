using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.Model
{
    public class User : EntityBase
    {
        [ForeignKey("UserDetail")]
        public int UserDetailsID { get; set; }
        public virtual UserDetails UserDetail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
        public virtual ICollection<Role> Roles { get; set; }
        public bool Active { get; set; }
    }
}
