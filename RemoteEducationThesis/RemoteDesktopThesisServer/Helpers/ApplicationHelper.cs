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
            Shortest = 2500,
            Short = 5000,
            Moderate = 10000,
            Long = 20000,
            Longest = 40000
        }

        #endregion
    }
}
