using System;
using System.Windows;

namespace RemoteDesktopThesisServer.Helpers
{
    public static class StyleHelper
    {
        #region Struct

        /// <summary>
        /// Contains keys for brushes.
        /// </summary>
        public struct BrushResourceKeys
        {
            public static string BetterWhiteBrush = "BetterWhiteBrush";
            public static string ApplicationMenuHoverBrush = "ApplicationMenuHoverBrush";
        }

        /// <summary>
        /// Contains keys for fonts.
        /// </summary>
        public struct FontResourceKeys
        {
            public static string ExtraTiny = "ExtraTinyFont";
            public static string Tiny = "TinyFont";
            public static string Small = "SmallFont";
            public static string Medium = "MediumFont";
            public static string Large = "LargeFont";
            public static string Larger = "LargerFont";
            public static string ExtraLarge = "ExtraLargeFont";
        }

        #endregion

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
