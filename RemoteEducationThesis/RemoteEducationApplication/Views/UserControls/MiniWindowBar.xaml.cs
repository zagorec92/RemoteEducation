using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Shared;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfDesktopFramework.Controls.Extensions;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MiniWindowBar.xaml
    /// </summary>
    public partial class MiniWindowBar : UserControlBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.MiniWindowBar"/>
        /// class.
        /// </summary>
        public MiniWindowBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Events & Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void RectangleClickEventHandler(object sender, ApplicationEventArgs e);

        /// <summary>
        /// RectangleClickEvent handler.
        /// </summary>
        public event RectangleClickEventHandler WindowBarClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes RectangleClickEventHandler.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        protected void OnAppBarClick(string commandName)
        {
            if (WindowBarClick != null)
                WindowBarClick(this, new ApplicationEventArgs(commandName: commandName));
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance
        /// containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sender.ExecuteIfNotNull<Rectangle>(x => OnAppBarClick(x.GetTag()));
        }

        #endregion
    }
}
