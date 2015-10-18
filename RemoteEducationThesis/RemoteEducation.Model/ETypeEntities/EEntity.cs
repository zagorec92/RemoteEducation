using Education.Model.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.ETypeEntities
{
	public abstract class EEntity : EntityBase
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[MaxLength(255)]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the active value.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public bool Active { get; set; }
	}
}
