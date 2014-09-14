﻿using RemoteEducationApplication.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using RemoteEducationApplication.Extensions;

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
            if (values.First() == DependencyProperty.UnsetValue)
                return Visibility.Hidden;

            string callingObjectCommandName = parameter == null ? String.Empty : parameter.ToString();
            Visibility visibility = (Visibility)values.First();
            
            if(visibility == Visibility.Visible)
            {
                bool isExpanded = bool.Parse(values.Last().ToString());

                if (callingObjectCommandName == ApplicationHelper.Commands.Expand)
                    return isExpanded ? Visibility.Hidden : Visibility.Visible;
                else if (callingObjectCommandName == ApplicationHelper.Commands.Shrink)
                    return isExpanded ? Visibility.Visible : Visibility.Hidden;

                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Hidden;
            }
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
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StatusTextVisibilityConverter :IValueConverter
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        private struct SenderType
        {
            public const string Other = "Other";
            public const string Text = "Text";
            public const string Image = "Image";
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

            switch(param)
            {
                case SenderType.Image:
                case SenderType.Other:
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
