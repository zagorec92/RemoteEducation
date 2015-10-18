using Education.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.ETypeEntities
{
	[Table("ERole")]
	public class Role : EEntity
	{
		/// <summary>
		/// Gets or sets the authorization level.
		/// </summary>
		public int AuthorizationLevel { get; set; }

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		public virtual ICollection<User> Users { get; set; }
	}
}
