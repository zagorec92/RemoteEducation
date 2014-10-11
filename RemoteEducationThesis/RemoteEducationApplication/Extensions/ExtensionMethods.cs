using RemoteEducationApplication.Client;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// <returns>Value of the CommandParameter property as a string.</returns>
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
        public static void NavigateTo(this Window window, Window navigateToWindow, bool isClosing)
        {
            if (isClosing)
            {
                window.Close();
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void ExecuteIfNotNull<T>(this object value, Action<T> action)
        {
            T item = To<T>(value);

            if (item != null)
                action.Invoke(item);
        }

        #endregion

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
        /// <returns></returns>
        public static int GetIndexOfValue<T>(T enumValue)
        {
            List<T> enumList = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            return enumList.IndexOf(enumValue);
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

        #region ObservableCollection

        #region Generic

        /// <summary>
        /// Moves item from current index to another.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="collection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance on which move is executed.</param>
        /// <param name="oldIndex">Current index.</param>
        /// <param name="newIndex">New index.</param>
        public static void MoveExtended<T>(this ObservableCollection<T> collection, int oldIndex, int newIndex)
        {
            T item = collection[oldIndex];
            collection.Remove(item);
            collection.Insert(newIndex, item);
        }

        /// <summary>
        /// Removes all items execpt the first one from existing collection and adds thme to another collection.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="addCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance in which items will be added.</param>
        /// <param name="removeCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance from which items will be removed.</param>
        public static void TakeExceptFirst<T>(this ObservableCollection<T> addCollection, 
            ObservableCollection<T> removeCollection)
        {
            int length = removeCollection.Count - 1;

            for (int i = length; i > 0; i--)
            {
                addCollection.Add(removeCollection[i]);
                removeCollection.RemoveAt(i);
            }
        }

        /// <summary>
        /// Removes all items from existing collection and adds them to another collection.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="addCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance in which items will be added.</param>
        /// <param name="removeCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance from which items will be removed.</param>
        public static void TakeAll<T>(this ObservableCollection<T> addCollection,
            ObservableCollection<T> removeCollection)
        {
            int length = removeCollection.Count - 1;

            for (int i = length; i > -1; i--)
            {
                addCollection.Add(removeCollection[i]);
                removeCollection.RemoveAt(i);
            }
        }

        #endregion

        #region ClientHandler

        /// <summary>
        /// Sorts the collection which contains clients.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="RemoteEducationApplication.Client.ClientHandler"/>.</typeparam>
        /// <param name="collection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> instance </param>
        public static void SortClient(this ObservableCollection<ClientHandler> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                for (int j = 0; j < collection.Count - i; j++)
                {
                    if (collection[j].ID > collection[j + 1].ID)
                        collection.MoveExtended(j, j + 1);

                    if (j == 0)
                        collection.MoveExtended(0, 0);
                }
            }
        }

        #endregion

        #endregion

        #region WebBrowser

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webBrowser"></param>
        /// <returns></returns>
        public static string GetQueryParams(this WebBrowser webBrowser)
        {
            return webBrowser.Source.Query.Length > 0 ? 
                webBrowser.Source.Query.Substring(1) : String.Empty;
        }

        #endregion

        #region String array

        /// <summary>
        /// Checks if the string array contains given string.
        /// </summary>
        /// <param name="array">The <see cref="System.String"/> array.</param>
        /// <param name="item">The <see cref="System.String"/> instance to compare.</param>
        /// <returns>True if array contains the item, otherwise false.</returns>
        public static bool Contains(this string[] array, string item)
        {
            foreach (string s in array)
                if (s.Equals(item))
                    return true;

            return false;
        }

        #endregion
    }
}
