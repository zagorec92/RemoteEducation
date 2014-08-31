using RemoteEducationApplication.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace RemoteEducationApplication.Converters
{
    public class WindowBarVisibilityConverter : IMultiValueConverter
    {
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

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
