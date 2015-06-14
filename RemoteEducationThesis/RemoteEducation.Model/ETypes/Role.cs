using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    [Table("ERole")]
    public class Role : ETableBase
    {
        /// <summary>
        /// Gets or sets the authorization level.
        /// </summary>
        public int AuthorizationLevel { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
