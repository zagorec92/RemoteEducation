using System.Windows;
using System.Windows.Controls;
using WpfDesktopFramework.DataTypes.Converters.Extensions;

namespace RemoteEducationApplication.Extensions
{
    public static class WebBrowserExtension
    {
        #region DependencyProperties

        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserExtension),
                new UIPropertyMetadata(null, BindableSourcePropertyChanged));
        
        #endregion

        #region EventHandling

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">The <see cref="System.Windows.DependencyObject"/> instance which is 
        /// the source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/>
        /// instance containing the event data.</param>
        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            o.ExecuteIfNotNull<WebBrowser>(x => x.NavigateToString(e.NewValue.ToString()));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">The <see cref="System.Windows.DependencyObject"/> instance.</param>
        /// <returns></returns>
        public static string GetBindableSource(DependencyObject obj)
        {
            return obj.GetValue(BindableSourceProperty).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">The <see cref="System.Windows.DependencyObject"/> instance.</param>
        /// <param name="value">The <see cref="System.String"/> value.</param>
        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        #endregion
    }
}
