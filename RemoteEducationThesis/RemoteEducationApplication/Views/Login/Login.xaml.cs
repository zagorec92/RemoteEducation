using RemoteEducation.DAL.Repositories;
using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RemoteEducationApplication.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : WindowBase
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        private struct WindowRoles
        {
            public static string Login = "LOGIN";
            public static string Recovery = "PASSWORD RECOVERY";
        }

        #endregion

        #region Fields

        private string _usernameValidationMessage;
        private string _passwordValidationMessage;
        private string _capsLockMessage;
        private string _windowRole;

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

        /// <summary>
        /// Gets or sets the window role.
        /// </summary>
        public string WindowRole
        {
            get 
            {
                return _windowRole;
            }
            set
            {
                _windowRole = value;
                NotifyPropertyChanged("WindowRole");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Login class.
        /// </summary>
        public Login()
        {
            WindowRole = WindowRoles.Login;
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

        /// <summary>
        /// Handles the RectangleClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance conatining the event data.</param>
        private void ApplicationBar_Click(object sender, ApplicationBarEventArgs e)
        {
            ApplicationHelper.ExecuteCommand(e.CommandName);
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

        /// <summary>
        /// Handles the MouseLeftButtonDow event of the textBlock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> 
        /// instance containing the event data.</param>
        private void textBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowRole = WindowRoles.Recovery;
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
                else if (bttn.GetCommandParameter() == ApplicationHelper.Commands.Cancel)
                {
                    tbxEmail.Text = String.Empty;
                    WindowRole = WindowRoles.Login;
                }
                else if (bttn.GetCommandParameter() == ApplicationHelper.Commands.Recover)
                    AuthenticationManager.RecoverPassword(tbxUsernameRecover.Text, tbxEmail.Text);
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
                //commented out for testing purposes
                //int roleID = AuthenticationManager.AuthenticateUser
                  //  (tbxUsername.Text, pbxPassword.Password);

                int roleID = 2;
                //user navigation to a new window, not null (when implemented)
                if (roleID == (int)RoleRepository.RoleType.Admin)
                    this.NavigateTo(new MainWindow(), true);
                else if (roleID == (int)RoleRepository.RoleType.User)
                    this.NavigateTo(null, true);
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
