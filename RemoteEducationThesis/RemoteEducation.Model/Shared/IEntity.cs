using System;

namespace Education.Model.Shared
{
	/// <summary>
	/// 
	/// </summary>
	public interface IEntity
	{
		int ID { get; set; }
		DateTime DateCreated { get; set; }
	}
}