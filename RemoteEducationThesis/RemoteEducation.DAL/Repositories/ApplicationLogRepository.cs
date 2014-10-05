using Education.Model;

namespace Education.DAL.Repositories
{
    public class ApplicationLogRepository : RepositoryBase<ApplicationLog>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.ApplicationLogRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public ApplicationLogRepository(EEducationDbContext context)
            : base(context) { }
    }
}
