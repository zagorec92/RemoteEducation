using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using System.Collections.Generic;

namespace RemoteEducationApplication.Helpers
{
    public static class SubjectHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Subject> GetSubjects()
        {
            List<Subject> subjects;
 
            using (EEducationDbContext context = new EEducationDbContext())
            {
                SubjectRepository subjectRepository = new SubjectRepository(context);
                subjects = new List<Subject>(subjectRepository.GetAll());
            }

            return subjects;
        }
    }
}
