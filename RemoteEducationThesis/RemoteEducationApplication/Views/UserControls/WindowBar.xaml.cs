using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Shared;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WindowRole = RemoteEducationApplication.Helpers.ApplicationHelper.WindowBarRole;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class ApplicationBar : UserControlBase
    {
        #region Dependecy properties

        /// <summary>
        /// IsExpandedDependencyProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedDependencyProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ApplicationBar));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the visibility of minimize icon.
        /// </summary>
        public WindowRole WindowRole { get; set; }

        /// <summary>
        /// Gets or sets the application bar visibility.
        /// </summary>
        public Visibility ApplicationBarVisibility { get; set; }

        /// <summary>
        /// Gets or sets the client bar visibility.
        /// </summary>
        public Visibility ClientBarVisibility { get; set; }

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
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void RectangleClickEventHandler(object sender, ApplicationBarEventArgs e);

        /// <summary>
        /// RectangleClickEvent handler.
        /// </summary>
        public event RectangleClickEventHandler AppBarClick;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.ApplicationBar"/> class.
        /// instance.
        /// </summary>
        public ApplicationBar()
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
            if(AppBarClick != null)
                AppBarClick(this, new ApplicationBarEventArgs(commandName: commandName));
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
                OnAppBarClick(rectangle.GetTag());
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
            }
        }

        #endregion
    }
}
