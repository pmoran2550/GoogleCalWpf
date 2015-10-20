using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Schuss.GoogleCalWpf.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToStringConverter : DependencyObject, IValueConverter
    {

        public static readonly DependencyProperty TrueStringProperty = DependencyProperty.Register("TrueString", typeof(string),
            typeof(BooleanToStringConverter), new PropertyMetadata("Green"));

        public static readonly DependencyProperty FalseStringProperty = DependencyProperty.Register("FalseString", typeof(string),
            typeof(BooleanToStringConverter), new PropertyMetadata("Red"));


        public string TrueString
        {
            get
            {
                return (string)GetValue(TrueStringProperty);
            }

            set
            {
                SetValue(TrueStringProperty, value);
            }
        }

        public string FalseString
        {
            get
            {
                return (string)GetValue(FalseStringProperty);
            }

            set
            {
                SetValue(FalseStringProperty, value);
            }
        }

        #region IValueConverter Members

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return FalseString;
            }
            else
            {
                return ((bool)value) ? TrueString : FalseString;
            }

        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
