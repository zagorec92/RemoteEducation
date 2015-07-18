using Education.Application.Shared;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFFramework.App.Base;
using WindowRole = Education.Application.Helpers.ApplicationHelper.WindowBarRole;

namespace Education.Application.Views.UserControls
{
	/// <summary>
	/// Interaction logic for ApplicationBar.xaml
	/// </summary>
	public partial class WindowBar : WpfUserControl
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
                OnPropertyChanged("ApplicationBarVisibility"); 
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
                OnPropertyChanged("ClientBarVisibility"); 
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
                OnPropertyChanged("MinimizeIconVisibility");
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
                OnPropertyChanged("IsExpanded");
            }
        }

		#endregion

		#region Events & Delegates

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationEventArgs"/>
		/// instance containing the event data.</param>
		public delegate void WindowBarClickEventHandler(object sender, ApplicationEventArgs e);

        /// <summary>
        /// RectangleClickEvent handler.
        /// </summary>
        public event WindowBarClickEventHandler WindowBarClick;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="Education.Application.Views.UserControls.WindowBar"/> class.
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
        /// Invokes WindowBarClickEventHandler.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        protected void OnAppBarClick(string commandName)
        {
            WindowBarClick.ExecuteSafe(x => x(this, new ApplicationEventArgs(commandName: commandName)));
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
