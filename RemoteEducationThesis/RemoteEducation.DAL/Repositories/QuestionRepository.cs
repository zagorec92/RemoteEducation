using Education.Model;
using System.Data.Entity;
using System.Linq;

namespace Education.DAL.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.QuestionRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public QuestionRepository(EEducationDbContext context)
            : base(context) { }

        /// <summary>
        /// Gets the question with answers.
        /// </summary>
        /// <param name="id">The <see cref="System.Int32"/> value representing question ID.</param>
        /// <returns></returns>
        public override Question Get(int id)
        {
            return base.GetAll()
                .Include(x => x.Answers)
                .First(x => x.ID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public IQueryable<Question> GetBySubject(int subjectId)
        {
            return base.GetAll()
                .Include(x => x.UploadedByUser)
                .Where(x => x.SubjectID == subjectId);
        }
    }
}
