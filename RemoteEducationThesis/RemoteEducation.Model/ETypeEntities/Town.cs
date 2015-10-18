using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.ETypeEntities
{
	[Table("ETown")]
	public class Town : EEntity
	{
		/// <summary>
		/// 
		/// </summary>
		[ForeignKey("Country")]
		public int CountryID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Country Country { get; set; }
	}
}
