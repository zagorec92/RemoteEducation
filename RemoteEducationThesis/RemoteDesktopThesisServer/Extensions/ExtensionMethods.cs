using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RemoteDesktopThesisServer.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the command parameter as a String.
        /// </summary>
        /// <param name="bttn">The <see cref="System.Windows.Controls.Button"/> 
        /// instance containing the command parameter.</param>
        /// <returns></returns>
        public static string GetCommandParameter(this Button bttn)
        {
            return bttn.CommandParameter.ToString();
        }
    }
}
