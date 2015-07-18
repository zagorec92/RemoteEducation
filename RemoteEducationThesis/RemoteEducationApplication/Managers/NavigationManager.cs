using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Helpers;
using ExtensionLibrary.NETFramework.Helpers;
using RemoteEducationApplication;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using WPFFramework.Attributes;

namespace Education.Application.Managers
{
    public static class NavigationManager
    {
        #region Properties

        

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="closeCurrentWindow"></param>
        public static void NavigateTo<T>(bool closeCurrentWindow)
            where T : Window, new()
        {
            App.WpfMainWindow.NavigateTo(new T(), closeCurrentWindow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="tag"></param>
        /// <param name="args"></param>
        public static void NavigateTo(Window mainWindow, string tag, params object[] args)
        {
            Assembly assembly = NETFrameworkHelper.GetAssembly<App>();

            foreach (Type type in assembly.GetTypes())
            {
                object[] attributes = type.GetCustomAttributes(typeof(MenuWindowAttribute), true);

                if (attributes.Any() && type.Name == tag)
                {
                    MenuWindowAttribute menuWindowsAttribute = (MenuWindowAttribute)attributes.First();

                    if (menuWindowsAttribute.UseDefaultConstructor)
                        mainWindow.NavigateTo((Window)DataTypesHelper.CreateInstance(type), false);
                    else
                        mainWindow.NavigateTo((Window)DataTypesHelper.CreateInstance(type, args), false);
                }
            }
        }

        #endregion
    }
}
