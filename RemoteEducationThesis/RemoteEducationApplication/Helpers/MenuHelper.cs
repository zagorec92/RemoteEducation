using RemoteEducationApplication.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace RemoteEducationApplication.Helpers
{
    public static class MenuHelper
    {
        /// <summary>
        /// Sets the font weight property of the MenuItem control to bold depending on the tag value.
        /// </summary>
        /// <param name="menuItem">The <see cref="System.Windows.Controls.MenuItem"/> instance
        /// containing the tag value.</param>
        public static void SetSelectedThemeNameInMenu(MenuItem menuItem)
        {
            ItemCollection itemCollection = menuItem.Items;

            foreach (object item in itemCollection)
            {
                MenuItem mItem = item as MenuItem;

                if (mItem != null)
                    mItem.FontWeight = mItem.GetTag().Equals(App.CurrentThemeName)
                        ? FontWeights.Bold : FontWeights.Normal;
            }
        }
    }
}
