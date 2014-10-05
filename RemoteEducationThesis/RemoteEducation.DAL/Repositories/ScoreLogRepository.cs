using Education.Model;
using System.Collections.Generic;
using System.Linq;

namespace Education.DAL.Repositories
{
    public class ScoreLogRepository : RepositoryBase<ScoreLog>
    {
        #region Enum

        /// <summary>
        /// Sort directions
        /// </summary>
        public enum SortDirection
        {
            Ascending = 1,
            Descending = 2,
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ScoreLogRepository(EEducationDbContext context)
            : base(context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the score based on username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Total score.</returns>
        public decimal GetScoreByUsername(string name)
        {
            ScoreLog scoreLog = base.GetAll().
                FirstOrDefault(x => x.User.FirstName.Equals(name));

            return scoreLog.TotalScore;
        }

        /// <summary>
        /// Sorts users based on score.
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
        /// Add points to the user.
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
        /// Subtract points from the user.
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

        #endregion
    }
}
