using System.Windows;
using System.Windows.Controls;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using Education.Application;

namespace Education.Application.Helpers
{
    public static class MenuHelper
    {
        /// <summary>
        /// Sets the font weight property of the MenuItem control to bold depending on the tag value.
        /// </summary>
        /// <param name="menuItem">The <see cref="System.Windows.Controls.MenuItem"/> instance containing the tag value.</param>
        /// <param name="isSubMenu"></param>
        public static void SetSelectedItemInMenu(MenuItem menuItem, bool isSubMenu)
        {
            if (isSubMenu)
            {
                ItemCollection itemCollection = menuItem.Items;

                foreach (object item in itemCollection)
                    item.ExecuteIfNotNull<MenuItem>(x => x.FontWeight = x.GetTag().Equals(App.CurrentThemeName) ? FontWeights.Bold : FontWeights.Normal);
            }
            else
            {
                menuItem.FontWeight = menuItem.FontWeight == FontWeights.Normal ? FontWeights.Bold : FontWeights.Normal;
            }
        }
    }
}
