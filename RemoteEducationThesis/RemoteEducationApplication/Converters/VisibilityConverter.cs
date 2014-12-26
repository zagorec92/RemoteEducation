using RemoteEducationApplication.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using WpfDesktopFramework.DataTypes.Converters.Extensions;

namespace RemoteEducationApplication.Converters
{
    public class WindowBarVisibilityConverter : IMultiValueConverter
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility retVal = Visibility.Hidden;

            if (values.First() != DependencyProperty.UnsetValue)
            {
                string callingObjectCommandName = parameter == null ? String.Empty : parameter.ToString();

                if (callingObjectCommandName != String.Empty)
                {
                    Visibility? visibility = values.First() as Visibility?;

                    if (visibility.Value == Visibility.Visible)
                    {
                        bool isExpanded = bool.Parse(values.Last().ToString());

                        if (callingObjectCommandName == ApplicationHelper.CommandTags.Expand)
                            retVal = isExpanded ? Visibility.Hidden : Visibility.Visible;
                        else if (callingObjectCommandName == ApplicationHelper.CommandTags.Shrink)
                            retVal = isExpanded ? Visibility.Visible : Visibility.Hidden;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("No need for converting back.");
        }

        #endregion
    }

    public class StatusTextVisibilityConverter : IValueConverter
    {
        #region Struct

        /// <summary>
        /// SenderType
        /// </summary>
        private struct SenderType
        {
            public const string Other = "Other";
            public const string Text = "Text";
            public const string Image = "Image";
            public const string Clock = "Clock";
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
            string param = parameter.ToString();
            Visibility visibility;

            bool condition = value.To<bool>();

            switch (param)
            {
                case SenderType.Image:
                case SenderType.Other:
                case SenderType.Clock:
                    visibility = condition ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case SenderType.Text:
                    visibility = condition ? Visibility.Collapsed : Visibility.Visible;
                    break;
                default:
                    visibility = Visibility.Collapsed;
                    break;
            }

            return visibility;
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
            throw new NotImplementedException("No need for converting back.");
        }

        #endregion
    }

    public class WindowTagConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Concat(values.First(),  values.Last());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("No need for converting back.");
        }
    }

}
