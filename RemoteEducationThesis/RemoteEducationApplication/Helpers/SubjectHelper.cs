using Education.DAL;
using Education.DAL.Repositories;
using Education.Model.Entities;
using System.Collections.Generic;

namespace Education.Application.Helpers
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
