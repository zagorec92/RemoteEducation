using Education.Model;
using System;
using System.Linq;

namespace RemoteEducation.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(EEducationDbContext context)
            : base(context) { }

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns></returns>
        public User Get(string name)
        {
            return base.GetAll()
                .FirstOrDefault(x => x.FirstName == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override bool InsertOrUpdate(User user)
        {
            try
            {
                user.UserDetail.DateCreated = 
                    user.UserDetail.DateModified = DateTime.Now;
                base.InsertOrUpdate(user);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
