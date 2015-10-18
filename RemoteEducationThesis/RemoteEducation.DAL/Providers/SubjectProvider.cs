using Education.DAL.Repositories;
using Education.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL.Providers
{
	public static class SubjectProvider
	{
		#region Methods

		#region Get

		#region Subject

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static List<Subject> GetAllSubjects()
		{
			List<Subject> retVal = null;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Subject> repository = new Repository<Subject>(context);
				IQueryable<Subject> subjects = repository
					.GetAll(x => x.Professor, x => x.Questions);

				if (subjects != null)
					retVal = subjects.ToList();
			}

			return retVal;
		}

		#endregion

		#region SubjectAttachment

		public static List<SubjectAttachment> GetSubjectAttachmentsBySubject(int subjectID)
		{
			List<SubjectAttachment> retVal = null;

			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<SubjectAttachment> repository = new Repository<SubjectAttachment>(context);
				IQueryable<SubjectAttachment> subjects = repository
					.GetAll(x => x.AttachmentType, x => x.Subject)
					.Where(x => x.SubjectID == subjectID);

				if (subjects != null)
					retVal = subjects.ToList();
			}

			return retVal;
		}

		#endregion

		#endregion

		#region Save

		#region Subject

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subject"></param>
		public static void Save(Subject subject)
		{
			using(EEducationDbContext context = new EEducationDbContext())
			{
				Repository<Subject> repository = new Repository<Subject>(context);

				if(repository.InsertOrUpdate(subject))
					repository.Save();
			}
		}

		#endregion

		#region SubjectAttachment

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subjectAttachment"></param>
		public static void SaveSubjectAttachment(SubjectAttachment subjectAttachment)
		{
			using (EEducationDbContext context = new EEducationDbContext())
			{
				Repository<SubjectAttachment> repository = new Repository<SubjectAttachment>(context);

				if (repository.InsertOrUpdate(subjectAttachment))
					repository.Save();
			}
		}

		public static void SaveSubjectAttachment(IEnumerable<SubjectAttachment> subjectAttachments)
		{
			foreach (SubjectAttachment subjectAttachment in subjectAttachments)
				SaveSubjectAttachment(subjectAttachment);
		}

		#endregion

		#endregion

		#endregion
	}
}
