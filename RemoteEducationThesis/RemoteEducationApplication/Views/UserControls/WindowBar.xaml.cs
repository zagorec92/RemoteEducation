using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Shared;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfDesktopFramework.Controls.Extensions;
using WindowRole = RemoteEducationApplication.Helpers.ApplicationHelper.WindowBarRole;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class WindowBar : UserControlBase
    {
        #region Fields

        private Visibility _applicationBarVisibility;
        private Visibility _clientBarVisibility;
        private Visibility _minimizeIconVisibility;

        #endregion

        #region Properties

        #region Dependecy properties

        /// <summary>
        /// IsExpandedDependencyProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedDependencyProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(WindowBar));

        #endregion

        /// <summary>
        /// Gets or sets the visibility of minimize icon.
        /// </summary>
        public WindowRole WindowRole { get; set; }

        /// <summary>
        /// Gets or sets the application bar visibility.
        /// </summary>
        public Visibility ApplicationBarVisibility 
        {
            get 
            { 
                return _applicationBarVisibility; 
            }
            set 
            { 
                _applicationBarVisibility = value; 
                NotifyPropertyChanged("ApplicationBarVisibility"); 
            } 
        }

        /// <summary>
        /// Gets or sets the client bar visibility.
        /// </summary>
        public Visibility ClientBarVisibility 
        {
            get 
            { 
                return _clientBarVisibility; 
            }
            set 
            { 
                _clientBarVisibility = value; 
                NotifyPropertyChanged("ClientBarVisibility"); 
            } 
        }

        /// <summary>
        /// Gets or sets the minimize icon visibility.
        /// </summary>
        public Visibility MinimizeIconVisibility
        {
            get
            {
                return _minimizeIconVisibility;
            }
            set
            {
                _minimizeIconVisibility = value;
                NotifyPropertyChanged("MinimizeIconVisibility");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the control is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return bool.Parse(GetValue(IsExpandedDependencyProperty).ToString());
            }
            set
            {
                SetValue(IsExpandedDependencyProperty, value);
                NotifyPropertyChanged("IsExpanded");
            }
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

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.WindowBar"/> class.
        /// instance.
        /// </summary>
        public WindowBar()
        {
            InitializeComponent();
            Loaded += ApplicationBar_Loaded;
        }

        #endregion

        #region EventHandling

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBar_Loaded(object sender, RoutedEventArgs e)
        {
            HandleRoleVisibility();
            DataContext = this;
        }

        /// <summary>
        /// Invokes RectangleClickEventHandler.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        protected void OnAppBarClick(string commandName)
        {
            if(WindowBarClick != null)
                WindowBarClick(this, new ApplicationEventArgs(commandName: commandName));
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rectangle element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        public virtual void Rectangle_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            sender.ExecuteIfNotNull<Rectangle>(x => OnAppBarClick(x.GetTag()));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines the visibility of icons based on WindowRole property.
        /// </summary>
        private void HandleRoleVisibility()
        {
            if (WindowRole == WindowRole.ApplicationBar)
            {
                ApplicationBarVisibility = Visibility.Visible;
                ClientBarVisibility = Visibility.Hidden;
            }
            else if (WindowRole == WindowRole.ClientBar)
            {
                ApplicationBarVisibility = Visibility.Hidden;
                ClientBarVisibility = Visibility.Visible;
                MinimizeIconVisibility = Visibility.Hidden;
            }
        }

        #endregion
    }
}
