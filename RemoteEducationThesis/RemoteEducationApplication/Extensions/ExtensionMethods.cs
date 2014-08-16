using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// Navigates to a new <see cref="System.Windows.Window"/> instance.
        /// </summary>
        /// <param name="window">The <see cref="System.Windows.Window"/>.</param>
        /// <param name="navigateToWindow">The <see cref="System.Windows.Window"/>.</param>
        /// <param name="isClosing"></param>
        public static void NavigateTo(this Window window, Window navigateToWindow, bool isClosing)
        {
            if(isClosing)
                window.Close();

            navigateToWindow.Show();
        }
    }
}
