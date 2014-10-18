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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Question Get(int id)
        {
            return base.GetAll()
                .Include("Answers")
                .First(x => x.ID == id);
        }
    }
}
