using System;
using System.Windows;

namespace RemoteEducationApplication.Helpers
{
    public static class StyleHelper
    {
        #region Methods

        /// <summary>
        /// Gets the font size.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>Font size.</returns>
        public static double GetFontSize(string key)
        {
            double retVal = 0;

            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[0];

            object fontSize = resourceDictionary[key];
            retVal = Convert.ToDouble(fontSize);

            return retVal;
        }

        #endregion
    }
}
