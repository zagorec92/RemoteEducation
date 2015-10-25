using Education.DAL.Repositories;
using Education.Model.ETypeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL.Providers
{
	public static class EProvider<T>
		where T : EEntity
	{
		#region Methods

		#region Get

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static List<T> GetAll()
		{
			List<T> retVal = null;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				ERepository<T> repository = new ERepository<T>(context);
				IQueryable<T> items = repository.GetAll();

				if (items != null)
					retVal = items.ToList();
			}

			return retVal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T Get(Predicate<T> predicate)
		{
			T retVal = null;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				ERepository<T> repository = new ERepository<T>(context);
				retVal = repository.Get(predicate);
			}

			return retVal;
		}

		#endregion

		#endregion
	}
}
