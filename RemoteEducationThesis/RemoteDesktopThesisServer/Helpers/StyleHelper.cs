using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xaml;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace RemoteEducationApplication.Helpers
{
    public static class StyleHelper
    {
        #region Methods

        /// <summary>
        /// Gets the font size.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns></returns>
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
