using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class Subject : Entity
	{
		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		public byte[] Image { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the professor ID.
		/// </summary>
		[ForeignKey("Professor")]
		public int ProfessorID { get; set; }

		/// <summary>
		/// Gets or sets the professor.
		/// </summary>
		public UserDetails Professor { get; set; }

		/// <summary>
		/// Gets or sets the collection of questions.
		/// </summary>
		public virtual ICollection<Question> Questions { get; set; }

		/// <summary>
		/// Gets or sets the collection of attachments.
		/// </summary>
		public virtual ICollection<SubjectAttachment> Attachments { get; set; }
	}
}
