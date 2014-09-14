using Education.Model;

namespace RemoteEducation.DAL.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>
    {
        public QuestionRepository(EEducationDbContext context)
            : base(context) { }
    }
}
