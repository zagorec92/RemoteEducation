using RemoteEducationApplication.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

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
        public static void NavigateTo(this Window window, Window navigateToWindow, bool isClosing)
        {
            if(isClosing)
                window.Close();

            Application.Current.MainWindow = navigateToWindow;
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
            Type type = typeof(T);

            return (T)Convert.ChangeType(frameworkElement.Tag, type);
        }

        #endregion

        #region BaseHelper.SleepTime

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int GetValue(this BaseHelper.SleepTime enumValue)
        {
            return (int)enumValue;
        }

        #endregion
    }
}
