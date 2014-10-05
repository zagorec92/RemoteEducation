using Education.Model;

namespace Education.DAL.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>
    {
        public AnswerRepository(EEducationDbContext context)
            : base(context) { }
    }
}
