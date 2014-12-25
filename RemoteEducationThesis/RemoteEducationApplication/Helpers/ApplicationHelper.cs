using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Views.Login;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using AppResources = RemoteEducationApplication.Properties.Resources;
using WpfDesktopFramework.Windows.Helpers;
using WpfDesktopFramework.Extensions;

namespace RemoteEducationApplication.Helpers
{
    public abstract class ApplicationHelper : BaseHelper
    {
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
        /// Logs off current user.
        /// </summary>
        public static void Logoff()
        {
            AuthenticationManager.LoggedInUser = null;
            App.Current.MainWindow.NavigateTo(new Login(), true);
        }

        #endregion

        #region Methods

        #region Basic Commands
        /// <summary>
        /// Executes given command on the main application window.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        public static void ExecuteBasicCommand(string command)
        {
            if (command == CommandTags.Minimize)
                WindowHelper.Minimize();
            else if (command == CommandTags.Close)
                WindowHelper.Close();
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

        #endregion

        #region Theme

        /// <summary>
        /// Checks if the given tag is a theme tag.
        /// </summary>
        /// <param name="tag">The <see cref="System.String"/> value representing the tag of the element.</param>
        /// <returns>True if the tag is theme tag, otherwise false.</returns>
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

        #endregion

        #region Menu

        /// <summary>
        /// Checks if the given header is a shared menu.
        /// </summary>
        /// <param name="header">The <see cref="System.String"/> value representing the header of the element.</param>
        /// <returns></returns>
        public static bool IsSharedMenu(string header)
        {
            string[] sharedMenu = new string[] 
            {
                AppResources.MenuHelpViewHelp,
                AppResources.MenuHelpAbout
            };

            return sharedMenu.Contains(header);
        }

        #endregion

        #endregion
    }
}
