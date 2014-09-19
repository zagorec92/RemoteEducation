using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RemoteEducationApplication.Extensions
{
    public static class ExtensionMethods
    {
        #region Button

        /// <summary>
        /// Gets the command parameter as a string.
        /// </summary>
        /// <param name="bttn">The <see cref="System.Windows.Controls.Button"/> 
        /// instance containing the command parameter.</param>
        /// <returns>CommandParameter as a string.</returns>
        public static string GetCommandParameter(this Button bttn)
        {
            return bttn.CommandParameter.ToString();
        }

        #endregion

        #region byte array

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

        /// <summary>
        /// Gets the byte array from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Gets the string from a byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);

            return new string(chars);
        }

        #endregion

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
        /// <param name="frameworkElement">The <see cref="System.Windows.FrameworkElement"/> instance.</param>
        /// <returns>Tag as string.</returns>
        public static string GetTag(this FrameworkElement frameworkElement)
        {
            return frameworkElement.Tag.ToString();
        }

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

        #region Convert object type

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

        #region DbContext

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsValid(this DbContext context)
        {
            if (context.Database.Connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        #endregion
    }
}
