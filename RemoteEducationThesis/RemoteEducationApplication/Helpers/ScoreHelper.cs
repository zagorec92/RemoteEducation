using Education.Model;
using Education.DAL;
using Education.DAL.Repositories;
using RemoteEducationApplication.Authentication;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RemoteEducationApplication.Helpers
{
    public static class ScoreHelper
    {
        /// <summary>
        /// Saves user score.
        /// </summary>
        /// <param name="score">The <see cref="System.Int32"/> value representing the score.</param>
        public static void SaveUserScore(int score)
        {
            using (EEducationDbContext context = new EEducationDbContext())
            {
                ScoreLogRepository scoreLogRepository = new ScoreLogRepository(context);
                ScoreLog scoreLog = new ScoreLog();
                scoreLog.TotalScore = score;
                scoreLog.UserID = AuthenticationManager.LoggedInUser.ID;

                if (scoreLogRepository.InsertOrUpdate(scoreLog))
                    scoreLogRepository.Save();
            }
        }

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <returns>The list of scores.</returns>
        public static List<ScoreLog> GetScoreLogs()
        {
            using (EEducationDbContext context = new EEducationDbContext())
            {
                ScoreLogRepository scoreLogRepository = new ScoreLogRepository(context);

                return scoreLogRepository.GetAll()
                    .Include("User")
                    .ToList();
            }
        }
    }
}