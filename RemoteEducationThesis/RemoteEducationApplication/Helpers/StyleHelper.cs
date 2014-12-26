using System;
using System.Windows;
using WpfDesktopFramework.Enums.Extensions;

namespace RemoteEducationApplication.Helpers
{
    public abstract class StyleHelper : BaseHelper
    {
        #region Const

        /// <summary>
        /// Represents theme folder path format string.
        /// </summary>
        private const string ThemeFolderPathFormat = "Resources/Style/Theme/{0}.xaml";

        #endregion

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

        /// <summary>
        /// Changes the application theme.
        /// </summary>
        /// <param name="themeName">
        /// <para>Theme name.</para>
        /// <para>Use the <c>ApplicationHelper.ThemeDictionaries</c> structure.</para>
        /// </param>
        public static void ChangeTheme(string themeName)
        {
            string dictionaryPath = String.Format(ThemeFolderPathFormat, themeName);

            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(dictionaryPath, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries[ResourceDictionaryIndex.Theme.GetValue()]
                = resourceDictionary;

            App.CurrentThemeName = themeName;
        }

        #endregion
    }
}
