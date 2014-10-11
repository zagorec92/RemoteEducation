using System;
using System.Windows;
using System.Windows.Controls;

namespace RemoteEducationApplication.Extensions
{
    public static class WebBrowserExtender
    {
        #region Properties

        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(Uri), typeof(WebBrowserExtender),
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
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                Uri uri = e.NewValue as Uri;
                browser.Source = uri;
            }
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
            return obj.GetValue(BindableSourceProperty).To<string>();
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
