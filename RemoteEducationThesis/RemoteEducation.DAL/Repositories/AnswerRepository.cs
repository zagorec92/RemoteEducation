using Education.Model;

namespace Education.DAL.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.AnswerRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public AnswerRepository(EEducationDbContext context)
            : base(context) { }
    }
}
