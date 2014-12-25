using RemoteEducationApplication.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfDesktopFramework.Extensions;

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
            Type type = Type.GetType(String.Concat("RemoteEducationApplication.Views.Menu.", windowClassIdentifier));
            window.NavigateTo((Window)Activator.CreateInstance(type, value), isClosing);
        }

        #endregion

        #region FrameworkElement

        /// <summary>
        /// Gets the Tag property of the <see cref="System.Windows.FrameworkElement"/> element as a custom type.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="frameworkElement">The <see cref="System.Windows.FrameworkElement"/> instance.</param>
        /// <returns>Tag as a custom type.</returns>
        public static T GetTag<T>(this FrameworkElement frameworkElement)
        {
            return To<T>(frameworkElement.Tag);
        }

        #endregion

        #region Object

        #region Convert

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this object value)
        {
            Type type = typeof(T);

            return (T)Convert.ChangeType(value, type);
        }

        #endregion

        #region ExecuteIfNotNull

        /// <summary>
        /// Converts object into given type and executes an action if object is not null.
        /// </summary>
        /// <typeparam name="T">The type to convert into.</typeparam>
        /// <param name="value">The <see cref="System.Object"/> value.</param>
        /// <param name="action">The <see cref="System.Action"/> to invoke.</param>
        public static void ExecuteIfNotNull<T>(this object value, Action<T> action)
        {
            T item = To<T>(value);

            if (item != null)
                action.Invoke(item);
        }

        #endregion

        #endregion

        #region Bitmap

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageSource GetImageSource(this Bitmap bitmap)
        {
            BitmapImage bitmapImage;

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();

            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.None;

            bitmapImage.EndInit();

            return bitmapImage;
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
