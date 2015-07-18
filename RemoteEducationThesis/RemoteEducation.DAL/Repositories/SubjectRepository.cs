using Education.Model.Entities;

namespace Education.DAL.Repositories
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(EEducationDbContext context)
            : base(context) { }
    }
}
