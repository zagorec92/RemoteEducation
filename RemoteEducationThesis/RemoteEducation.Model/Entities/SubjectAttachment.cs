using Education.Model.ETypeEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class SubjectAttachment : Entity
	{
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		public byte[] Content { get; set; }

		/// <summary>
		/// Gets or sets the subject ID.
		/// </summary>
		[ForeignKey("Subject")]
		public int SubjectID { get; set; }

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		public Subject Subject { get; set; }

		/// <summary>
		/// Gets or sets the attachment type ID.
		/// </summary>
		[ForeignKey("AttachmentType")]
		public int AttachmentTypeID { get; set; }

		/// <summary>
		/// Gets or sets the attachment type.
		/// </summary>
		public AttachmentType AttachmentType { get; set; }
	}
}
