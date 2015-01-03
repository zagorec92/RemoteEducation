using Education.Model;

namespace Education.DAL.Repositories
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(EEducationDbContext context)
            : base(context) { }
    }
}
