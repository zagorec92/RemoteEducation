using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class Session : Entity
	{
		/// <summary>
		/// Identifier
		/// </summary>
		[Index(IsUnique = true)]
		public Guid Identifier { get; set; }

		/// <summary>
		/// SubjectID
		/// </summary>
		[ForeignKey("Subject")]
		public int SubjectID { get; set; }

		/// <summary>
		/// Subject
		/// </summary>
		public Subject Subject { get; set; }

		/// <summary>
		/// DateStart
		/// </summary>
		public DateTime? DateStart { get; set; }

		/// <summary>
		/// DateEnd
		/// </summary>
		public DateTime? DateEnd { get; set; }

		/// <summary>
		/// Gets or sets the collection of scores in session.
		/// </summary>
		public virtual ICollection<Score> Scores { get; set; }
	}
}
