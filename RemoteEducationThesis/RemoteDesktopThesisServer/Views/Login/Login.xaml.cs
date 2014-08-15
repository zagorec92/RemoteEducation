using RemoteEducationApplication.ApplicationManagement;
using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RemoteEducationApplication.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {
        #region Fields

        private string _usernameValidationMessage;
        private string _passwordValidationMessage;
        private string _capsLockMessage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the message to be displayed for username TextBox.
        /// </summary>
        public string UsernameValidationMessage 
        {
            get 
            {
                return _usernameValidationMessage; 
            }
            set 
            {
                _usernameValidationMessage = value;
                NotifyPropertyChanged("UsernameValidationMessage");
            } 
        }

        /// <summary>
        /// Gets or sets the message to be displayed for PasswordBox.
        /// </summary>
        public string PasswordValidationMessage
        {
            get
            {
                return _passwordValidationMessage;
            }
            set
            {
                _passwordValidationMessage = value;
                NotifyPropertyChanged("PasswordValidationMessage");
            }
        }

        /// <summary>
        /// Gets or sets the message to be displayed when Caps Lock is toggled.
        /// </summary>
        public string CapsLockMessage
        {
            get
            {
                return _capsLockMessage;
            }
            set
            {
                _capsLockMessage = value;
                NotifyPropertyChanged("CapsLockMessage");
            }
        }

        #endregion

        #region Events
        
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Login class.
        /// </summary>
        public Login()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            DataContext = this;
        }

        #endregion

        #region EventHandlers

        #region Window

        /// <summary>
        /// Overrides the OnInitialized event of the Login window.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //for testing
            //ApplicationManager appManager = new ApplicationManager(this);
            //appManager.Start();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AuthenticateUser();
        }

        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region TextControl

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordValidationMessage = String.Empty;

            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
                CapsLockMessage = "CAPS LOCK is on.";
            else
                CapsLockMessage = String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernameValidationMessage = String.Empty;
        }

        #endregion

        #region LoginMenu

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

                if (commandName == ApplicationManager.Commands.Close)
                    ApplicationManager.Close();
                else if (commandName == ApplicationManager.Commands.Minimize)
                    ApplicationManager.Minimize();
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
            {
                rectangle.Fill =
                    FindResource("ApplicationMenuHoverBrush") as SolidColorBrush;
            }
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
            {
                rectangle.Fill = 
                    FindResource("BetterWhiteBrush") as SolidColorBrush;
            }
        }

        #endregion

        #region Button

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CapsLockMessage = String.Empty;
            Button bttn = sender as Button;

            if (bttn != null)
            {
                if (bttn.GetCommandParameter() == ApplicationManager.Commands.Clear)
                    tbxUsername.Text = pbxPassword.Password = String.Empty;
                else if (bttn.GetCommandParameter() == ApplicationManager.Commands.Login)
                    AuthenticateUser();
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Authenticates user.
        /// </summary>
        private void AuthenticateUser()
        {
            try
            {
                AuthenticationManager.AuthenticateUser
                    (tbxUsername.Text, pbxPassword.Password);

                //redirect
            }
            catch (ArgumentException ex)
            {
                string message = ExceptionHelper.GetMessage(ex);

                if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsUsername)
                    UsernameValidationMessage = message;
                else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsPassword)
                    PasswordValidationMessage = message;
                else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsParameters)
                    UsernameValidationMessage = PasswordValidationMessage = message;
            }
        }

        #endregion
    }
}
