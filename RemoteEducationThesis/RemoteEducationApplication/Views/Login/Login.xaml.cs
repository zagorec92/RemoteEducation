using Education.Application.Helpers;
using Education.Application.Managers;
using Education.Application.Managers.Authentication;
using Education.Application.Shared;
using Education.Application.Views.Client;
using Education.Application.Views.Server;
using Education.DAL;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Extensions;
using ExtensionLibrary.Exceptions.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppResources = Education.Application.Properties.Resources;

namespace Education.Application.Views.Login
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : WindowBase
    {
        #region Struct

        /// <summary>
        /// Window titles.
        /// </summary>
        private struct WindowTitles
        {
            public static string Login = AppResources.LoginPageLoginTitle;
            public static string Recovery = AppResources.LoginPageRecoveryTitle;
        }

        #endregion

        #region Fields

        private string _usernameValidationMessage;
        private string _passwordValidationMessage;
        private string _capsLockMessage;
        private string _windowRole;
        private string _username;
        private string _securityCode;
        private string _loadingScreenText;
        private bool _isLoading;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the message to be displayed for username TextBox.
        /// </summary>
        public string UsernameValidationMessage 
        {
            get { return _usernameValidationMessage; }
            set 
            {
                _usernameValidationMessage = value;
                OnPropertyChanged(this, x => x.UsernameValidationMessage);
            } 
        }

        /// <summary>
        /// Gets or sets the message to be displayed for PasswordBox.
        /// </summary>
        public string PasswordValidationMessage
        {
            get { return _passwordValidationMessage; }
            set
            {
                _passwordValidationMessage = value;
                OnPropertyChanged(this, x => x.PasswordValidationMessage);
            }
        }

        /// <summary>
        /// Gets or sets the message to be displayed when Caps Lock is toggled.
        /// </summary>
        public string CapsLockMessage
        {
            get { return _capsLockMessage; }
            set
            {
                _capsLockMessage = value;
                OnPropertyChanged(this, x => x.CapsLockMessage);
            }
        }

        /// <summary>
        /// Gets or sets the window role.
        /// </summary>
        public string WindowRole
        {
            get { return _windowRole; }
            set
            {
                _windowRole = value;
                OnPropertyChanged(this, x => x.WindowRole);
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(this, x => x.Username);
            }
        }

        /// <summary>
        /// Gets or sets the security code.
        /// </summary>
        public string SecurityCode
        {
            get { return _securityCode; }
            set
            {
                _securityCode = value;
                OnPropertyChanged(this, x => x.SecurityCode);
            }
        }

        /// <summary>
        /// Gets or sets the text that is displayed on loading screen.
        /// </summary>
        public string LoadingScreenText
        {
            get { return _loadingScreenText; }
            set
            {
                _loadingScreenText = value;
                OnPropertyChanged(this, x => x.LoadingScreenText);
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if application is loading information.
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(this, x => x.IsLoading);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Login class.
        /// </summary>
        public Login()
        {
            InitializeStartupParameters();
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        #endregion

        #region EventHandlers

        #region Window

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance
        /// containing the event data.</param>
        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
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
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void ApplicationBar_Click(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName, this);
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
        /// Handles the MouseLeftButtonDown event of the TextBlock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> 
        /// instance containing the event data.</param>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetWindowTitle(WindowTitles.Recovery);
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

            sender.ExecuteIfNotNull<Button>(bttn =>
            {
                string commandParameter = bttn.GetCommandParameter();

                if (commandParameter == ApplicationHelper.CommandTags.Clear)
                    Username = pbxPassword.Password = String.Empty;
                else if (commandParameter == ApplicationHelper.CommandTags.Login)
                    AuthenticateUser();
                else if (commandParameter == ApplicationHelper.CommandTags.Cancel)
                    SetWindowTitle(WindowTitles.Login);
                else if (commandParameter == ApplicationHelper.CommandTags.Recover)
                    ResetPassword();
            });
        }

        #endregion

        #endregion

        #region Methods

        #region Authentication

        /// <summary>
        /// Authenticates user and initializes application module depending on the role.
        /// </summary>
        private async void AuthenticateUser()
        {
            //throw new Exception("aksj");
            try
            {
				int roleID;

                UpdateLoadingScreen(loadingText: AppResources.LoginPageLoginLoadingText);

				//roleID = await Task.Run(() => AuthenticationManager.AuthenticateUser(Username, pbxPassword.Password));

				//loading screen test
				await Task.Delay(BaseHelper.SleepTime.Short.GetValue());

				roleID = EnumCollection.RoleType.Admin.GetValue(); //test

				UpdateLoadingScreen(loadingText: "Uspješna prijava");
				await Task.Delay(BaseHelper.SleepTime.Shortest.GetValue());

				if (roleID == EnumCollection.RoleType.Admin.GetValue() ||
                    roleID == EnumCollection.RoleType.Professor.GetValue())
                    NavigationManager.NavigateTo<MainWindow>(true);
                else if (roleID == EnumCollection.RoleType.Student.GetValue())
                    NavigationManager.NavigateTo<ClientWindow>(true);
            }
            catch (ArgumentException ex)
            {
                UpdateLoadingScreen(isVisible: false);
                string message = ExceptionHelper.GetMessage(ex);

                if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsUsername)
                    UsernameValidationMessage = message;
                else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsPassword)
                    PasswordValidationMessage = message;
                else if (ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsParameters ||
                    ex.ParamName == AuthenticationManager.AuthenticateExParameters.IsDatabase)
                    UsernameValidationMessage = PasswordValidationMessage = message;
            }
        }

        /// <summary>
        /// Resets password.
        /// </summary>
        private async void ResetPassword()
        {
            SetWindowTitle(WindowTitles.Login);

            try
            {
                UpdateLoadingScreen(loadingText: AppResources.LoginPageLoginLoadingText);
                await Task.Run(() => AuthenticationManager.RecoverPassword(Username, SecurityCode));
                UpdateLoadingScreen(loadingText: AppResources.LoginPageRecoverySuccessText);
            }
            catch (ArgumentException ae)
            {
                UpdateLoadingScreen(loadingText: ExceptionHelper.GetShortMessage(ae));
            }

            await Task.Delay(BaseHelper.SleepTime.Shortest.GetValue());
            UpdateLoadingScreen(isVisible: false);
        }

		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowTitle"></param>
        private void SetWindowTitle(string windowTitle)
        {
            WindowRole = windowTitle;
        }

        /// <summary>
        /// Sets the visibility and the display text of the loading control.
        /// </summary>
        /// <param name="isVisible"></param>
        private void UpdateLoadingScreen(bool isVisible = true, string loadingText = null)
        {
            LoadingScreenText = loadingText ?? String.Empty;
            IsLoading = isVisible;
        }

        /// <summary>
        /// Initializes parameters.
        /// </summary>
        private void InitializeStartupParameters()
        {
            SetWindowTitle(WindowTitles.Login);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

		#endregion
    }
}
