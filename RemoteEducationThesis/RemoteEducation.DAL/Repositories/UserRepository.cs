using Education.Model;
using System;
using System.Linq;
using System.Data.Entity;

namespace RemoteEducation.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(EEducationDbContext context)
            : base(context) { }

        #endregion

        #region Get

        /// <summary>
        /// Gets the user by first name.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns></returns>
        public User GetByFirstName(string firstName)
        {
            return base.GetAll()
                .FirstOrDefault(x => x.FirstName.Equals(firstName));
        }

        /// <summary>
        /// Gets the user by last name.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public User GetByLastName(string lastName)
        {
            return base.GetAll()
                .FirstOrDefault(x => x.LastName.Equals(lastName));
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return base.GetAll()
                .FirstOrDefault(x => x.UserDetail.Email.Equals(email));
        }

        #endregion
    }
}
