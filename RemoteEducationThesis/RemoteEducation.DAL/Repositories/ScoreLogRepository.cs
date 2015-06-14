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
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.ScoreLogRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public ScoreLogRepository(EEducationDbContext context)
            : base(context) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the score based on username.
        /// </summary>
        /// <param name="username">The <see cref="System.String"/> value representing username.</param>
        /// <returns>The <see cref="System.Decimal"/> value.</returns>
        public decimal GetScoreByUsername(string name)
        {
            ScoreLog scoreLog = base.GetAll()
                .FirstOrDefault(x => x.User.UserDetail.FirstName.Equals(name));

            return scoreLog.TotalScore;
        }

        /// <summary>
        /// Sorts users based on score.
        /// </summary>
        /// <param name="sortDirection">The <see cref="Education.DAL.Repositories.ScoreLogRepository.SortDirection"/>
        /// value representing sort direction.</param>
        /// <typeparam name="T">T is <see cref="Education.Model.User"/>.</typeparam>
        /// <returns>The <see cref="System.Collections.Generic.IEnumerable{T}"/> collection.</returns>
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
        /// <param name="id">The <see cref="System.Int32"/> value representing score ID.</param>
        /// <param name="points">The <see cref="System.Decimal"/> value representing points.</param>
        /// <returns>The <see cref="System.Boolean"/> value indicating if the operation succeded.</returns>
        public bool AddPoints(int id, decimal points)
        {
            ScoreLog scoreLog = base.Get(id);
            scoreLog.TotalScore += points;

            return base.InsertOrUpdate(scoreLog);
        }

        /// <summary>
        /// Subtract points from the user.
        /// </summary>
        /// <param name="id">The <see cref="System.Int32"/> value representing score ID.</param>
        /// <param name="points">The <see cref="System.Decimal"/> value representing points.</param>
        /// <returns>The <see cref="System.Boolean"/> value indicating if the operation succeded.</returns>
        public bool SubtractPoints(int id, decimal points)
        {
            ScoreLog scoreLog = base.Get(id);
            scoreLog.TotalScore -= points;

            return base.InsertOrUpdate(scoreLog);
        }

        #endregion
    }
}
