using Education.DAL.Repositories;
using Education.Model.Entities;
using System.Collections.Generic;

namespace Education.DAL.Providers
{
	public static class AnswerProvider
	{
		/// <summary>
		/// Saves an answer.
		/// </summary>
		/// <param name="answer">The <see cref="Education.Model.Answer"/> instance.</param>
		public static void Save(Answer answer)
		{
			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Answer> repository = new Repository<Answer>(context);

				if (repository.InsertOrUpdate(answer))
					repository.Save();
			}
		}

		/// <summary>
		/// Saves a collection of answers.
		/// </summary>
		/// <typeparam name="T">T is <see cref="Education.Model.Answer"/>.</typeparam>
		/// <param name="answers">The <see cref="System.Collections.Generic.ICollection{T}"/> collection containing instances of type T.</param>
		public static void Save(ICollection<Answer> answers)
		{
			foreach (Answer answer in answers)
				Save(answer);
		}
	}
}
