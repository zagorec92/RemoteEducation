using RemoteDesktopThesisServer.ApplicationManagement;
using RemoteDesktopThesisServer.Authentication;
using RemoteDesktopThesisServer.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RemoteDesktopThesisServer.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        #region Fields

        private const int _validationSleepTime = 5000;

        #endregion

        #region Properties

        /// <summary>
        /// Font size used for regular text.
        /// </summary>
        public int TextFontSize { get; set; }

        /// <summary>
        /// Font size used for validation text.
        /// </summary>
        public int ValidationFontSize { get; set; }

        #endregion

        #region Constructor

        public Login()
        {
            InitializeComponent();

            TextFontSize = (int)StyleHelper.FontSize.Medium;
            ValidationFontSize = (int)StyleHelper.FontSize.Small;

            DataContext = this;
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Overrides the OnInitialized event of the Login window.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //for testing
            ApplicationManager appManager = new ApplicationManager(this);
            appManager.Start();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                string commandName = rectangle.Tag.ToString();

                if (commandName == "Close")
                    Application.Current.Shutdown();
                else if (commandName == "Minimize")
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
                rectangle.Fill = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// Handles the MouseLeave event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
                rectangle.Fill = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bttn = sender as Button;

            if (bttn != null)
            {
                if (bttn.CommandParameter.ToString() == "Clear")
                {
                    tbxUsername.Text = String.Empty;
                    pbxPassword.Password = String.Empty;
                }
                else if (bttn.CommandParameter.ToString() == "Login")
                {
                    try
                    {
                        bool isAuthenticated = AuthenticationManager.AuthenticateUser
                            (tbxUsername.Text, pbxPassword.Password);

                        if (isAuthenticated)
                        { /*redirect*/ }
                    }
                    catch (ArgumentException ex)
                    {
                        string message = ex.Message.Substring(0, ex.Message.IndexOf('.') + 1);

                        if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsUsername)
                            tbkValidationUsername.Text = message;
                        else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsPassword)
                            tbkValidationPassword.Text = message;
                        else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsParametersEmpty)
                            tbkValidationUsername.Text = tbkValidationPassword.Text = message;
                    }

                    await Task.Delay(_validationSleepTime);

                    tbkValidationUsername.Text = String.Empty;
                    tbkValidationPassword.Text = String.Empty;
                }
            }
        }

        #endregion
    }
}
