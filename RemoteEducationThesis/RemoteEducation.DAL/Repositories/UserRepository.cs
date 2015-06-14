using Education.Model;
using System.Linq;
using System.Data.Entity;

namespace Education.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.UserRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public UserRepository(EEducationDbContext context)
            : base(context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets user by first name.
        /// </summary>
        /// <param name="firstName">The <see cref="System.String"/> value representing first name.</param>
        /// <returns>The <see cref="Education.Model.User"/> instance if found, null otherwise.</returns>
        public User GetByFirstName(string firstName)
        {
            return base.GetAll()
                .Include(x => x.UserDetail)
                .FirstOrDefault(x => x.UserDetail.FirstName.Equals(firstName));
        }

        /// <summary>
        /// Gets user by last name.
        /// </summary>
        /// <param name="lastName">The <see cref="System.String"/> value representing last name.</param>
        /// <returns>The <see cref="Education.Model.User"/> instance if found, null otherwise.</returns>
        public User GetByLastName(string lastName)
        {
            return base.GetAll()
                .Include(x => x.UserDetail)
                .FirstOrDefault(x => x.UserDetail.LastName.Equals(lastName));
        }

        /// <summary>
        /// Gets user by email.
        /// </summary>
        /// <param name="email">The <see cref="System.String"/> value representing email address</param>
        /// <returns>The <see cref="Education.Model.User"/> instance if found, null otherwise.</returns>
        public User GetByUsername(string email)
        {
            return base.GetAll()
                .Include(x => x.UserDetail)
                .FirstOrDefault(x => x.UserDetail.Email.Equals(email));
        }

        #endregion
    }
}
