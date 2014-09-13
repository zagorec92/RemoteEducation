using Education.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public class ScoreLogRepository : RepositoryBase<ScoreLog>
    {
        public enum SortDirection
        {
            Ascending = 1,
            Descending = 2,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ScoreLogRepository(EEducationDbContext context)
            : base(context) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public decimal GetScoreByUsername(string name)
        {
            ScoreLog scoreLog = base.GetAll().
                FirstOrDefault(x => x.User.FirstName.Equals(name));

            return scoreLog.TotalScore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public IEnumerable<User> SortUsersByScore(SortDirection sortDirection)
        {
            IEnumerable<User> users = null;

            switch(sortDirection)
            {
                case SortDirection.Ascending:
                    users = base.GetAll().
                        OrderBy(x => x.TotalScore).Select(x => x.User);
                    break;
                case SortDirection.Descending:
                    users = base.GetAll().
                        OrderByDescending(x => x.TotalScore).Select(x => x.User);
                    break;
            }

            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool AddPoints(int id, decimal points)
        {
            ScoreLog scoreLog = base.Get(id);
            scoreLog.TotalScore += points;

            return base.InsertOrUpdate(scoreLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool SubtractPoints(int id, decimal points)
        {
            ScoreLog scoreLog = base.Get(id);
            scoreLog.TotalScore -= points;

            return base.InsertOrUpdate(scoreLog);
        }
    }
}
