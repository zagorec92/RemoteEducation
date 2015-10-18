using Education.Model.ETypeEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public class Log : Entity
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		public Guid? UserIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the log type.
		/// </summary>
		[ForeignKey("LogType")]
		public int LogTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public LogType LogType { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the stack trace.
		/// </summary>
		public string StackTrace { get; set; }
	}
}
