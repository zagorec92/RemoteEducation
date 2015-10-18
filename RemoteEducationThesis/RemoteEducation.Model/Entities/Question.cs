using Education.Model.ETypeEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class Question : Entity
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the HTML content of the question.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the question difficulty.
		/// </summary>
		public double Difficulty { get; set; }

		/// <summary>
		/// Gtes or sets the ID of the user who uploaded the question.
		/// </summary>
		[ForeignKey("UploadedByUser")]
		public int UploadedBy { get; set; }

		/// <summary>
		/// Gets or sets the user who uploaded the question.
		/// </summary>
		public User UploadedByUser { get; set; }

		/// <summary>
		/// Gets or sets the subject id.
		/// </summary>
		[ForeignKey("Subject")]
		public int SubjectID { get; set; }

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		public Subject Subject { get; set; }

		/// <summary>
		/// Gets or sets the question type id.
		/// </summary>
		[ForeignKey("QuestionType")]
		public int QuestionTypeID { get; set; }

		/// <summary>
		/// Gets or sets the question type.
		/// </summary>
		public QuestionType QuestionType { get; set; }

		/// <summary>
		/// Gets or sets the value indicating if answer should be automatically evaluated.
		/// </summary>
		public bool AutomaticEvaluation { get; set; }

		/// <summary>
		/// Gets or sets the collection of correct answers.
		/// </summary>
		public virtual ICollection<Answer> Answers { get; set; }
	}
}
