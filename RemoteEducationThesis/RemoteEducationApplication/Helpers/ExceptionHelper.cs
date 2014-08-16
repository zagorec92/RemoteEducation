using System;

namespace RemoteEducationApplication.Helpers
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Removes extra content from exception message.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/> instance containing the message.</param>
        /// <returns>Message substring.</returns>
        public static string GetMessage(Exception ex)
        {
            return ex.Message.Substring(0, ex.Message.IndexOf('\r'));
        }
    }
}
