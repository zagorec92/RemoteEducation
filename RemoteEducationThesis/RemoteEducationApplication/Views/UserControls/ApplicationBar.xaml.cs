using RemoteEducationApplication.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// 
    /// </summary>
    public class RectangleEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Initializes a new <see cref="RemoteEducationApplication.Views.UserControls.RectangleEventArgs"/>
        /// instance.
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
        /// Gets or sets the visibility of minimize icon.
        /// </summary>
        public Visibility MinimizeVisibility { get; set; }

        #endregion

        #region Events & Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Views.UserControls.RectangleEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void RectangleClickEventHandler(object sender, RectangleEventArgs e);

        /// <summary>
        /// RectangleClickEvent handler.
        /// </summary>
        public event RectangleClickEventHandler RectangleClick;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="RemoteEducationApplication.Views.UserControls.ApplicationBar"/>
        /// instance.
        /// </summary>
        public ApplicationBar()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes RectangleClickEventHandler.
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
                OnRectangleClick(rectangle.GetTag());
        }

        #endregion
    }
}
