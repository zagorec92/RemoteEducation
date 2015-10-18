using Education.DAL.Repositories;
using Education.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Education.DAL.Providers
{
	public static class QuestionProvider
	{
		#region Methods

		#region Get

		/// <summary>
		/// Gets the question by ID.
		/// </summary>
		/// <param name="questionID">The <see cref="System.Int32"/> value.</param>
		/// <returns>The <see cref="Education.Model.Question"/> instance if succeded, null otherwise.</returns>
		public static Question Get(int questionID)
		{
			Question question = null;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Question> repository = new Repository<Question>(context);
				question = repository
					.GetAll(x => x.Answers)
					.First(x => x.ID == questionID);
			}

			return question;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subjectID"></param>
		/// <returns></returns>
		public static List<Question> GetBySubject(int subjectID)
		{
			List<Question> retVal = new List<Question>();

			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Question> repository = new Repository<Question>(context);
				IQueryable<Question> questions = repository
					.GetAll(x => x.UploadedBy, x => x.Answers, x => x.Subject)
					.Where(x => x.SubjectID == subjectID);

				if (questions != null)
					retVal = questions.ToList();
			}

			return retVal;
		}

		#endregion

		#region Save

		/// <summary>
		/// Saves question in database.
		/// </summary>
		/// <param name="question">The <see cref="Education.Model.Question"/> instance.</param>
		public static void Save(Question question)
		{
			if (question != null && !String.IsNullOrEmpty(question.Content))
			{
				using (EEducationDbContext context = new EEducationDbContext())
				{
					Repository<Question> repository = new Repository<Question>(context);

					if (repository.InsertOrUpdate(question))
						repository.Save();
				}
			}
			else
			{
				throw new ArgumentException("Question or question content cannot be null or empty.");
			}
		}

		#endregion

		#endregion
	}
}
