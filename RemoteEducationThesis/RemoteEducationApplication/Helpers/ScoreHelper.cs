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
        /// 
        /// </summary>
        /// <param name="score"></param>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ScoreLog> GetScoreLogs()
        {
            using(EEducationDbContext context = new EEducationDbContext())
            {
                ScoreLogRepository scoreLogRepository = new ScoreLogRepository(context);

                return scoreLogRepository.GetAll()
                    .Include("User")
                    .ToList();
            }
        }
    }
}