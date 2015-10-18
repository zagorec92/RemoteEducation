using Education.DAL.Repositories;
using Education.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Education.DAL.Providers
{
	public static class UserProvider
	{
		#region Properties

		/// <summary>
		/// Gets or sets the current user.
		/// </summary>
		public static User LoggedInUser { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static IList<User> GetAll()
		{
			IList<User> users = null;

			using(EEducationDbContext context = new EEducationDbContext())
			{
				Repository<User> repository = new Repository<User>(context);
				users = repository.GetAll(x => x.UserDetail, x => x.Roles).ToList();
			}

			return users;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public static User Get(string username)
		{
			User user = null;

			using(EEducationDbContext context = new EEducationDbContext())
			{
				Repository<User> repository = new Repository<User>(context);

				user = repository
					.GetAll(x => x.UserDetail, x => x.Roles)
					.FirstOrDefault(x => x.UserDetail.Email.Equals(username));
			}

			return user;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		public static void Save(User user)
		{
			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<User> repository = new Repository<User>(context);

				if (repository.InsertOrUpdate(user))
					repository.Save();
			}
		}

		#endregion
	}
}
