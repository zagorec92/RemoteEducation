using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Linq;
using System;
using System.Windows;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// 
    /// </summary>
    public class RectangleEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public RectangleEventArgs(string name)
            : base()
        {
            CommandName = name;
        }
    }

    /// <summary>
    /// Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class ApplicationBar : UserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the flag indicating if the control is used for global control of App state.
        /// </summary>
        public bool IsMainWindowAppBar { get; set; }

        #endregion

        #region Events & Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void RectangleClickEventHandler(object sender, RectangleEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        public event RectangleClickEventHandler RectangleClick;

        #endregion

        #region Constructor

        public ApplicationBar()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandling

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void OnRectangleClick(string command)
        {
            if(RectangleClick != null)
                RectangleClick(this, new RectangleEventArgs(command));
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rectangle element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        public virtual void Rectangle_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                string commandName = rectangle.GetTag();

                if (IsMainWindowAppBar)
                {
                    if (commandName == ApplicationHelper.Commands.Close)
                        ApplicationHelper.Close();
                    else if (commandName == ApplicationHelper.Commands.Minimize)
                        ApplicationHelper.Minimize();
                }
                else 
                {
                    OnRectangleClick(commandName);
                }
            }
        }

        #endregion
    }
}
