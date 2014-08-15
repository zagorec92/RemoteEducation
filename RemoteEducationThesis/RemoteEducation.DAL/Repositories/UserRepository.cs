using RemoteEducation.Model;
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
        public UserRepository(RemoteEducationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns></returns>
        public User Get(int id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns></returns>
        public User Get(string username)
        {
            return base.GetAll()
                .FirstOrDefault(x => x.UserDetail.Username == username);
        }

        public bool InsertOrUpdate(User user)
        {
            try
            {
                user.UserDetail.DateCreated = user.UserDetail.DateModified = DateTime.Now;
                user.Active = true;
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
