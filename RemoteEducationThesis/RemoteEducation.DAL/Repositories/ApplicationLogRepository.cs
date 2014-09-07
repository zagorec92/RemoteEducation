using Education.Model;

namespace RemoteEducation.DAL.Repositories
{
    public class ApplicationLogRepository : RepositoryBase<ApplicationLog>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducation.DAL.Repositories.ApplicationLogRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="RemoteEducation.DAL.EEducationDbContext"/> instance.</param>
        public ApplicationLogRepository(EEducationDbContext context)
            : base(context) { }
    }
}
