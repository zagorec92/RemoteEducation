using ExtensionLibrary.Collections.Extensions;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.Controls.Helpers;
using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Views.Login;
using System;
using System.Collections.Generic;
using System.Windows;
using AppResources = RemoteEducationApplication.Properties.Resources;

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

			public static string QuestionUpload = AppResources.SubMenuQuestionUpload;
			public static string QuestionSelect = AppResources.SubMenuQuestionSelect;
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

		#region Properties

		private static Dictionary<string, Action> CommandMapping { get; set; }
		private static Dictionary<string, Action<Window>> WindowCommandMapping { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public static void InitializeCommandMappings()
		{
			CommandMapping = new Dictionary<string, Action>();
			CommandMapping.Add(CommandTags.Minimize, () => WindowHelper.Minimize());
			CommandMapping.Add(CommandTags.Close, () => App.CloseApplication());
			CommandMapping.Add(CommandTags.Logoff, () => Logoff());

			WindowCommandMapping = new Dictionary<string, Action<Window>>();
			WindowCommandMapping.Add(CommandTags.Close, x => x.Close());
			WindowCommandMapping.Add(CommandTags.Minimize, x => x.WindowState = WindowState.Minimized);
		}

		#region BasicAppCommands

		/// <summary>
		/// Logs off current user.
		/// </summary>
		public static void Logoff()
		{
			AuthenticationManager.Logout();
			App.WpfMainWindow.NavigateTo(new Login(), true);
		}

		#endregion

		#region Basic Commands

		/// <summary>
		/// Executes given command on the main application window.
		/// </summary>
		/// <param name="command">Command to execute.</param>
		public static void ExecuteBasicCommand(string command)
        {
			if (CommandMapping.ContainsKey(command))
				CommandMapping[command].Invoke();
			else
				throw new KeyNotFoundException("Command does not exist or its behaviour is not implemented.");
        }

        /// <summary>
        /// Executes given command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="window">The <see cref="System.Windows.Window"/> instance on which the execution of the 
        /// command will be performed.</param>
        public static void ExecuteBasicCommand(string command, Window window)
		{
			if (WindowCommandMapping.ContainsKey(command))
				WindowCommandMapping[command].Invoke(window);
			else
				throw new KeyNotFoundException("Command does not exist or its behaviour is not implemented.");
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
			if (String.IsNullOrEmpty(header))
				throw new ArgumentException("Menu header cannot be null or empty.");

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
