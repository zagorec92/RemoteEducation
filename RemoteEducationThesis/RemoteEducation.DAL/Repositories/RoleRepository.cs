using Education.Model;

namespace Education.DAL.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        #region Enum

        public enum RoleType
        {
            /// <summary>
            /// Admin role
            /// </summary>
            Admin = 1,

            /// <summary>
            /// Student role
            /// </summary>
            Student = 2,

            /// <summary>
            /// Proffesor role
            /// </summary>
            Professor = 3
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
