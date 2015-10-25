using Education.DAL;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Extensions;
using System;
using System.Windows;
using System.Windows.Media;

namespace Education.Application.Managers
{
	public static class StyleManager
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
			double retVal = default(double);
			
			ResourceDictionary resourceDictionary = GetResourceDictionary(EnumCollection.ResourceDictionaryIndex.FontSize);
			resourceDictionary.ExecuteSafe(x => x[key].ExecuteSafe(y => retVal = y.To<double>()));

			return retVal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static SolidColorBrush GetBrush(string key)
		{
			SolidColorBrush retVal = null;

			ResourceDictionary resourceDictionary = GetResourceDictionary(EnumCollection.ResourceDictionaryIndex.Brushes);
			resourceDictionary.ExecuteSafe(x => x[key].ExecuteSafe(y => retVal = (SolidColorBrush)y));

			return retVal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resourceDictionaryIndex"></param>
		/// <returns></returns>
		private static ResourceDictionary GetResourceDictionary(EnumCollection.ResourceDictionaryIndex resourceDictionaryIndex)
		{
			return App.MergedDictionaries[resourceDictionaryIndex.GetValue()];
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
			if (!String.IsNullOrEmpty(themeName))
			{
				string dictionaryPath = String.Format(ThemeFolderPathFormat, themeName);

				ResourceDictionary resourceDictionary = new ResourceDictionary();
				resourceDictionary.Source = new Uri(dictionaryPath, UriKind.Relative);

				App.MergedDictionaries[EnumCollection.ResourceDictionaryIndex.Theme.GetValue()] = resourceDictionary;
				App.CurrentThemeName = themeName;
			}
		}

		#endregion
	}
}
