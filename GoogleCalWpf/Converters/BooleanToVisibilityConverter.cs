using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Schuss.GoogleCalWpf.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueVisibleProperty = DependencyProperty.Register("TrueVisible", typeof(Visibility),
            typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty FalseVisibleProperty = DependencyProperty.Register("FalseVisible", typeof(Visibility),
            typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Hidden));


        public Visibility TrueVisible
        {
            get
            {
                return (Visibility)GetValue(TrueVisibleProperty);
            }

            set
            {
                SetValue(TrueVisibleProperty, value);
            }
        }

        public Visibility FalseVisible
        {
            get
            {
                return (Visibility)GetValue(FalseVisibleProperty);
            }

            set
            {
                SetValue(FalseVisibleProperty, value);
            }
        }

        #region IValueConverter Members

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null )
            {
                return FalseVisible;
            }
            else
            {
                return ((bool)value) ? TrueVisible : FalseVisible;
            }

        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
