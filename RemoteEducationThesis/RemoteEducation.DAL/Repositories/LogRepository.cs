using Education.Model;

namespace Education.DAL.Repositories
{
    public class LogRepository : RepositoryBase<Log>
    {
		#region Enum

		public enum LogType
		{
			Error = 1,
			Info = 2
		}

		#endregion

		/// <summary>
		/// Creates a new instance of the <see cref="Education.DAL.Repositories.LogRepository"/> class.
		/// </summary>
		/// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
		public LogRepository(EEducationDbContext context)
            : base(context) { }
    }
}
