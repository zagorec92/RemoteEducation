using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Views.Dialog;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        /// The <see cref="System.ComponentModel.PropertyChangedEventHandler"/> delegate.
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
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Handles the KeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/>
        /// instance containing the event data.</param>
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
        /// Handles the PasswordChanged event of the pbxPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> 
        /// instance containing the event data.</param>
        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordValidationMessage = String.Empty;

            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
                CapsLockMessage = "CAPS LOCK is on.";
            else
                CapsLockMessage = String.Empty;
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/>
        /// instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernameValidationMessage = String.Empty;
        }

        #endregion

        #region LoginMenu

        /// <summary>
        /// Handles the MouseLeftButtonDown event of Rectangle element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                string commandName = rectangle.GetTag();

                if (commandName == ApplicationHelper.Commands.Close)
                    ApplicationHelper.Close();
                else if (commandName == ApplicationHelper.Commands.Minimize)
                    ApplicationHelper.Minimize();
            }
        }

        #endregion

        #region Button

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> 
        /// instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CapsLockMessage = String.Empty;
            Button bttn = sender as Button;

            if (bttn != null)
            {
                if (bttn.GetCommandParameter() == ApplicationHelper.Commands.Clear)
                    tbxUsername.Text = pbxPassword.Password = String.Empty;
                else if (bttn.GetCommandParameter() == ApplicationHelper.Commands.Login)
                    AuthenticateUser();
            }
        }

        #endregion

        #region HyperLink

        /// <summary>
        /// Handles the Click event of the HyperLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> 
        /// instance containing the event data.</param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new AuthenticationDataRecovery(), false);
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
                ///commented out for testing purposes
                //AuthenticationManager.AuthenticateUser
                //    (tbxUsername.Text, pbxPassword.Password);

                this.NavigateTo(new MainWindow(), true);
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
