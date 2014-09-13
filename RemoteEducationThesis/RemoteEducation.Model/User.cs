using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Model
{
    public class User : EntityBase
    {
        /// <summary>
        /// UserDetails ID.
        /// </summary>
        [ForeignKey("UserDetail")]
        public int UserDetailsID { get; set; }

        /// <summary>
        /// The <see cref="RemoteEducation.Model.UserDetails"/> instance.
        /// </summary>
        public virtual UserDetails UserDetail { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// First and last name combined.
        /// </summary>
        public string FullName 
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}
