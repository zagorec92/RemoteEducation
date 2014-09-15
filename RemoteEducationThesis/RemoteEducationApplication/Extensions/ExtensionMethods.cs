using RemoteEducationApplication.Helpers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.Generic;

namespace RemoteEducationApplication.Extensions
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

        /// <summary>
        /// Checks if byte arrays are equal by compairing every byte.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayToCompare">Array to compare.</param>
        /// <returns>True if arrays are equal.</returns>
        public static bool EqualsByByte(this byte[] array, byte[] arrayToCompare)
        {
            if (array.Length != arrayToCompare.Length)
                return false;

            for (int i = 0; i < array.Length; i++)
                if (array[i] != arrayToCompare[i])
                    return false;

            return true;
        }

        #region Window

        /// <summary>
        /// Navigates to a new <see cref="System.Windows.Window"/> instance.
        /// </summary>
        /// <param name="window">The <see cref="System.Windows.Window"/>.</param>
        /// <param name="navigateToWindow">The <see cref="System.Windows.Window"/>.</param>
        /// <param name="isClosing">Close current window.</param>
        public static void NavigateTo(this Window control, Window navigateToWindow, bool isClosing)
        {
            if (isClosing)
            {
                control.Close();
                Application.Current.MainWindow = navigateToWindow;
            }

            navigateToWindow.Show();
        }

        #endregion

        #region FrameworkElement

        /// <summary>
        /// Gets the Tag property of the <see cref="System.Windows.FrameworkElement"/> element as string.
        /// </summary>
        /// <param name="rectangle">The <see cref="System.Windows.FrameworkElement"/> instance.</param>
        /// <returns>Tag as string.</returns>
        public static string GetTag(this FrameworkElement frameworkElement)
        {
            return frameworkElement.Tag.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        public static T GetTag<T>(this FrameworkElement frameworkElement)
        {
            return To<T>(frameworkElement.Tag);
        }

        #endregion

        #region Convert object

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

        #region Enum

        /// <summary>
        /// Gets the Int32 value from enum.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int GetValue(this Enum enumValue)
        {
            return (int)Enum.Parse(enumValue.GetType(), enumValue.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetValueByIndex<T>(int index)
        {
            List<T> enumList = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            return enumList[index];
        }

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

        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
