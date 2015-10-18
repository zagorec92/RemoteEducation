using Education.DAL.Repositories;
using Education.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Education.DAL.Providers
{
	public static class ScoreProvider
	{
		#region Methods

		#region Get

		public static decimal GetByUsername(string name)
		{
			decimal scoreValue = default(decimal);

			using(EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Score> repository = new Repository<Score>(context);
				IEnumerable<Score> scores = repository
					.GetAll(x => x.User, x => x.User.UserDetail)
					.Where(x => x.User.UserDetail.Email.Equals(name));

				if(scores != null)
					scoreValue = scores.Sum(x => x.Points);
			}

			return scoreValue;
		}

		/// <summary>
		/// Gets the scores.
		/// </summary>
		/// <returns>The list of scores.</returns>
		public static List<Score> GetAll()
		{
			List<Score> scores;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Score> repository = new Repository<Score>(context);
				scores = new List<Score>();

				IQueryable<Score> scoresFromDb = repository.GetAll(x => x.User, x => x.User.UserDetail);

				if (scoresFromDb != null)
					scores = scoresFromDb.ToList();
			}

			return scores;
		}

		#endregion

		#region Save

		/// <summary>
		/// Saves user score.
		/// </summary>
		/// <param name="score">The <see cref="System.Int32"/> value representing the score.</param>
		public static void SaveUserScore(Score score)
		{
			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Score> repository = new Repository<Score>(context);

				if (repository.InsertOrUpdate(score))
					repository.Save();
			}
		}

		#endregion

		#endregion
	}
}