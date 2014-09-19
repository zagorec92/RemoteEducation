using System;
using System.Windows;
using RemoteEducationApplication.Extensions;
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Helpers
{
    public abstract class ApplicationHelper : BaseHelper
    {
        #region Struct

        /// <summary>
        /// Contains command names.
        /// </summary>
        public struct Commands
        {
            public static string Login = AppResources.LoginPageLogin;
            public static string Recover = AppResources.RecoveryAction;

            public static string Close = AppResources.WindowBarClose;
            public static string Minimize = AppResources.WindowBarMinimize;
            public static string Cancel = AppResources.CancelAction;
            public static string Clear = AppResources.LoginPageClear;
            
            public static string Connect = AppResources.WindowBarConnect;
           
            public static string Expand = AppResources.WindowBarExpand;
            public static string Shrink = AppResources.WindowBarShrink;

            public static string Question = AppResources.OptionsSubMenuOpenQuestion;
        }

        /// <summary>
        /// Contains theme dictionary names.
        /// </summary>
        public struct ThemeDictionaries
        {
            public static string Default = "DefaultTheme";
            public static string Orange = "OrangeTheme";
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

        #endregion

        #region Methods

        /// <summary>
        /// Executes given command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        public static void ExecuteBasicCommand(string command)
        {
            if (command == Commands.Minimize)
                Minimize();
            else if (command == Commands.Close)
                Close();
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
            string dictionaryPath = String.Format("Resources/Style/Theme/{0}.xaml", themeName);

            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(dictionaryPath, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries[ResourceDictionaryIndex.Theme.GetValue()] 
                = resourceDictionary;
        }

        #endregion
    }
}
