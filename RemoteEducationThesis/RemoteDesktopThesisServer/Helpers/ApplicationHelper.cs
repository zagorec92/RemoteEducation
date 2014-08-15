using System.Windows;
namespace RemoteEducationApplication.Helpers
{
    public abstract class ApplicationHelper
    {
        #region Enum

        /// <summary>
        /// Sleep time.
        /// </summary>
        public enum SleepTime
        {
            /// <summary>
            /// 2500 ms
            /// </summary>
            Shortest = 2500,

            /// <summary>
            /// 5000 ms
            /// </summary>
            Short = 5000,

            /// <summary>
            /// 10000 ms
            /// </summary>
            Moderate = 10000,

            /// <summary>
            /// 20000 ms
            /// </summary>
            Long = 20000,

            /// <summary>
            /// 40000 ms
            /// </summary>
            Longest = 40000
        }

        #endregion

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
    }
}
