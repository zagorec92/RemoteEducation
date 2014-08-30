using System.Windows;

namespace RemoteEducationApplication.Helpers
{
    public abstract class ApplicationHelper : BaseHelper
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        public struct Commands
        {
            public static string Clear = "Clear";
            public static string Login = "Login";
            public static string Close = "Close";
            public static string Minimize = "Minimize";
            public static string Cancel = "Cancel";
            public static string Recover = "Recover";
        }

        #endregion

        #region Enum

        public enum WindowBarRole
        {
            ApplicationBar = 1,
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
        public static void ExecuteCommand(string command)
        {
            if (command == Commands.Minimize)
                Minimize();
            else if (command == ApplicationHelper.Commands.Close)
                Close();
        }

        #endregion
    }
}
