using Education.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>
    {
        public AnswerRepository(EEducationDbContext context)
            : base(context) { }
    }
}
