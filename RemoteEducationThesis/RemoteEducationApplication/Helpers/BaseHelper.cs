namespace RemoteEducationApplication.Helpers
{
    public abstract class BaseHelper
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

        /// <summary>
        /// Resource dictionary indexes from App.xaml.
        /// </summary>
        public enum ResourceDictionaryIndex
        {
            /// <summary>
            /// FontSize resource dictionary.
            /// </summary>
            FontSize = 0,
            
            /// <summary>
            /// Brushes resource dictionary.
            /// </summary>
            Brushes = 1,

            /// <summary>
            /// DefaultTheme resource dictionary.
            /// </summary>
            Theme = 2,

            /// <summary>
            /// GlobalStyle resource dictionary.
            /// </summary>
            GlobalStyle = 3,

            /// <summary>
            /// LoginStyle resource dictionary.
            /// </summary>
            LoginStyle = 4,

            /// <summary>
            /// MainWindowStyle resource dictionary.
            /// </summary>
            MainWindowStyle = 5,

            /// <summary>
            /// ClientWindowStyle resource dictionary.
            /// </summary>
            ClientWindowStyle = 6,

            /// <summary>
            /// UserControlStyle resource dictionary.
            /// </summary>
            UserControlStyle = 7,

            /// <summary>
            /// LoginStoryboards resource dictionary.
            /// </summary>
            LoginStoryboards = 8
        }

        #endregion
    }
}
