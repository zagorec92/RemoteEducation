using RemoteEducationApplication.Client;
using System;
using System.Net.Sockets;
using System.Windows;
using WpfDesktopFramework.Controls.Extensions;
using WpfDesktopFramework.Enums.Extensions;

namespace RemoteEducationApplication.Extensions
{
    public static class GeneralExtensions
    {
        #region Window

        /// <summary>
        /// Navigates to a new <see cref="System.Windows.Window"/> instance.
        /// </summary>
        /// <param name="window">The <see cref="System.Windows.Window"/>.</param>
        /// <param name="windowClassIdentifier">The <see cref="System.String"/> value representing the class name.</param>
        /// <param name="isClosing">The <see cref="Systm.Bool"/> value indicating if 
        /// the current window should be closed.</param>
        /// <remarks>Only used for menu items which open a new window.</remarks>
        public static void NavigateTo(this Window window, string windowClassIdentifier, bool isClosing, object[] value)
        {
            Type type = Type.GetType(String.Concat(App.MenuWindowPath, windowClassIdentifier));
            window.NavigateTo((Window)Activator.CreateInstance(type, value), isClosing);
        }

        #endregion

        #region ClientHandler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientHandler"></param>
        /// <returns></returns>
        public static bool IsClientConnected(this ClientHandler clientHandler)
        {
            if (clientHandler.IsClientConnected)
                return true;
            else
                throw new SocketException(SocketError.ConnectionAborted.GetValue());
        }

        #endregion
    }
}
