using Education.Application.Helpers;
using Education.Application.Shared;
using System.Windows;
using WPFFramework.Attributes;

namespace Education.Application.Views.Menu.Help
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    [MenuWindow]
    public partial class Help : WindowBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string WindowTitle { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instace of the <see cref="RemoteEducationApplication.Views.Menu.Help"/> class.
        /// </summary>
        public Help()
        {
            InitializeComponent();
            Loaded += Help_Loaded;
        }

        /// <summary>
        /// Initializes a new instace of the <see cref="RemoteEducationApplication.Views.Menu.Help"/> class.
        /// </summary>
        /// <param name="windowTitle">The <see cref="System.String"/> value representing the window title.</param>
        public Help(string windowTitle) 
            : this()
        {
            WindowTitle = windowTitle;
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event of the Help control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Help_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        #endregion

        #region WindowBar

        /// <summary>
        /// Handles the AppBarClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/> instance
        /// containing the event data.</param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName, this);
        }

        #endregion

        #endregion
    }
}
