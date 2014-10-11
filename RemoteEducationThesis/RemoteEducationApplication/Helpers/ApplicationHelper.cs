using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Views.Login;
using System;
using System.Windows;
using System.Windows.Controls;
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Helpers
{
    public abstract class ApplicationHelper : BaseHelper
    {
        #region Const

        /// <summary>
        /// Represents theme folder path format string.
        /// </summary>
        private const string ThemeFolderPathFormat = "Resources/Style/Theme/{0}.xaml";

        #endregion

        #region Struct

        /// <summary>
        /// Contains command names.
        /// </summary>
        public struct CommandTags
        {
            public static string Login = AppResources.LoginPageLogin;
            public static string Logoff = AppResources.MenuFileLogoff;
            public static string Recover = AppResources.RecoveryAction;

            public static string Close = AppResources.WindowBarClose;
            public static string Minimize = AppResources.WindowBarMinimize;
            public static string Cancel = AppResources.CancelAction;
            public static string Clear = AppResources.LoginPageClear;
            public static string FullScreen = AppResources.SubMenuViewFullScreen;
            
            public static string Connect = AppResources.WindowBarConnect;
           
            public static string Expand = AppResources.WindowBarExpand;
            public static string Shrink = AppResources.WindowBarShrink;

            public static string Question = AppResources.OptionsSubMenuOpenQuestion;
            public static string ScoreList = AppResources.OptionsSubMenuScoreList;
        }

        #endregion

        #region Enum

        /// <summary>
        /// 
        /// </summary>
        public enum WindowBarRole
        {
            /// <summary>
            /// 
            /// </summary>
            ApplicationBar = 1,

            /// <summary>
            /// 
            /// </summary>
            ClientBar = 2
        }

        #endregion

        #region BasicAppCommands

        /// <summary>
        /// Closes the application.
        /// </summary>
        public static void Close()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Minimizes window.
        /// </summary>
        public static void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Logs off current user.
        /// </summary>
        public static void Logoff()
        {
            AuthenticationManager.LoggedInUser = null;
            App.Current.MainWindow.NavigateTo(new Login(), true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes given command on the main application window.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        public static void ExecuteBasicCommand(string command)
        {
            if (command == CommandTags.Minimize)
                Minimize();
            else if (command == CommandTags.Close)
                Close();
            else if (command == CommandTags.Logoff)
                Logoff();
        }

        /// <summary>
        /// Executes given command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="window">The <see cref="System.Windows.Window"/> instance on which the execution of the 
        /// command will be performed.</param>
        public static void ExecuteBasicCommand(string command, Window window)
        {
            if (command == CommandTags.Close)
                window.Close();
            else if (command == CommandTags.Minimize)
                window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Chekcs if the given tag is a theme tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>True if tthe tag is theme tag, otherwise false.</returns>
        public static bool IsThemeTag(string tag)
        {
            string[] themes = new string[] 
            {
                AppResources.MenuThemeOrange,
                AppResources.MenuThemeDark,
                AppResources.MenuThemeClassic
            };

            return themes.Contains(tag);
        }

        /// <summary>
        /// Change the application theme.
        /// </summary>
        /// <param name="themeName">
        /// <para>Theme name.</para>
        /// <para>Use the <c>ApplicationHelper.ThemeDictionaries</c> structure.</para>
        /// </param>
        public static void ChangeTheme(string themeName)
        {
            string dictionaryPath = String.Format(ThemeFolderPathFormat, themeName);

            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(dictionaryPath, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries[ResourceDictionaryIndex.Theme.GetValue()] 
                = resourceDictionary;

            App.CurrentThemeName = themeName;
        }

        #endregion
    }
}
