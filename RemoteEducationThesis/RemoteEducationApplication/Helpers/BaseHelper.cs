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

        #endregion
    }
}
