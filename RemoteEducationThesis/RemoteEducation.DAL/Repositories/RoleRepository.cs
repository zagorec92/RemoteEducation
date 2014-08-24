using RemoteEducation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public enum RoleType
        {
            /// <summary>
            /// User role
            /// </summary>
            User = 1,

            /// <summary>
            /// Admin role
            /// </summary>
            Admin = 2
        }
    }
}
