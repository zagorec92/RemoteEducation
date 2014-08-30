using RemoteEducation.Model;

namespace RemoteEducation.DAL.Repositories
{
    public class ApplicationLogRepository : RepositoryBase<ApplicationLog>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducation.DAL.Repositories.ApplicationLogRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="RemoteEducation.DAL.RemoteEducationDbContext"/> instance.</param>
        public ApplicationLogRepository(RemoteEducationDbContext context)
            : base(context) { }
    }
}
