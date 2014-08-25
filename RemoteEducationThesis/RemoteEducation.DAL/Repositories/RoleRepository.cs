using RemoteEducation.Model;

namespace RemoteEducation.DAL.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        #region Enum

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

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(RemoteEducationDbContext context)
            : base(context) { }

        #endregion
    }
}
