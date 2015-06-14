using Education.Model;

namespace Education.DAL.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        #region Enum

        /// <summary>
        /// Role types.
        /// </summary>
        public enum RoleType
        {
            /// <summary>
            /// Admin role
            /// </summary>
            Admin = 1,

            /// <summary>
            /// Professor role
            /// </summary>
            Professor = 2,

            /// <summary>
            /// Student role
            /// </summary>
            Student = 3
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.RoleRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public RoleRepository(EEducationDbContext context)
            : base(context) { }

        #endregion
    }
}
