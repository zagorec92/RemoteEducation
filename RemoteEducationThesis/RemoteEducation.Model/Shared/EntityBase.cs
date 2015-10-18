using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Shared
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class EntityBase
	{
		/// <summary>
		/// Gets or sets the entity ID.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets the date created.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime DateCreated { get; set; }
	}
}