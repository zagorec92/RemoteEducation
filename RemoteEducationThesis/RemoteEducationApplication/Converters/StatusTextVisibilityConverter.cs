using RemoteEducationApplication.Extensions;
using System;
using System.Windows;
using System.Windows.Data;

namespace RemoteEducationApplication.Converters
{
    public class StatusTextVisibilityConverter :IValueConverter
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        private struct SenderType
        {
            public static string Text = "Text";
            public static string Image = "Image";
        }

        #endregion

        #region IValueConverterMethods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool hasPicture = value.To<bool>();

            if (parameter.ToString().Equals(SenderType.Image))
            {
                if (hasPicture)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else if (parameter.ToString().Equals(SenderType.Text))
            {
                if (hasPicture)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
