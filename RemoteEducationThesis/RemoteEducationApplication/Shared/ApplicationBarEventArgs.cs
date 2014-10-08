using System;

namespace RemoteEducationApplication.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationEventArgs : EventArgs
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
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.RectangleEventArgs"/> class.
        /// instance.
        /// </summary>
        /// <param name="commandName">Name of the command.<c>Optional.</c></param>
        /// <param name="objectName">Name of the object.<c>Optional.</c></param>
        public ApplicationEventArgs(string commandName = "", int objectID = 0)
            : base()
        {
            CommandName = commandName;
            ObjectID = objectID;
        }

        #endregion
    }
}
