using System;

namespace Education.Application.Shared
{
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationBarEventArgs : EventArgs
	{
		#region Properties

		/// <summary>
		/// Gets or sets the command name.
		/// </summary>
		public string CommandName { get; set; }

		/// <summary>
		/// Gets or sets the object name.
		/// </summary>
		public int ObjectID { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="Education.Application.Views.UserControls.RectangleEventArgs"/> class.
		/// </summary>
		/// <param name="commandName">Name of the command.<c>Optional.</c></param>
		/// <param name="objectName">Name of the object.<c>Optional.</c></param>
		public ApplicationBarEventArgs(string commandName = "", int objectID = 0)
			: base()
		{
			CommandName = commandName;
			ObjectID = objectID;
		}

		#endregion
	}
}
