using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktopThesisServer.Helpers
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Removes extra content from exception message.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/> instance containing the message.</param>
        /// <returns></returns>
        public static string GetMessage(Exception ex)
        {
            return ex.Message.Substring(0, ex.Message.IndexOf('\r'));
        }
    }
}
