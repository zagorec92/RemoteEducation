using Education.Model;
using System.Linq;
using System.Data.Entity;

namespace RemoteEducation.DAL.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>
    {
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
