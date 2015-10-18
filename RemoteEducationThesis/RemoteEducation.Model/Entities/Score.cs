using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class Score : Entity
	{
		/// <summary>
		/// Gets or sets the user ID.
		/// </summary>
		[ForeignKey("User")]
		public int UserID { get; set; }

		/// <summary>
		/// Gets or sets the question ID.
		/// </summary>
		[ForeignKey("Question")]
		public int QuestionID { get; set; }

		/// <summary>
		/// Gets or sets the session identifier.
		/// </summary>
		[ForeignKey("Session")]
		public Guid? SessionID { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Gets or sets the question.
		/// </summary>
		public Question Question { get; set; }

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// Gets or sets the total score.
		/// </summary>
		public decimal Points { get; set; }
	}
}
