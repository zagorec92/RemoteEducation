using Education.Model.Shared;
using System;

namespace Education.Model.Entities
{
	public abstract class Entity : EntityBase
	{
		/// <summary>
		/// Gets or sets the date modified.
		/// </summary>
		public DateTime? DateModified { get; set; }
	}
}
