using Education.DAL.Repositories;
using Education.Model.Entities;

namespace Education.DAL.Providers
{
	public static class LogProvider
	{
		#region Save

		/// <summary>
		/// 
		/// </summary>
		/// <param name="log"></param>
		public static void Save(Log log)
		{
			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Log> repository = new Repository<Log>(context);

				if (repository.InsertOrUpdate(log))
					repository.Save();
			}
		}

		#endregion
	}
}
