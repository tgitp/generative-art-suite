﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Vortex.GenerativeArtSuite.Common.Converters
{
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public Visibility OnNotNull { get; set; } = Visibility.Visible;

        public Visibility OnNull { get; set; } = Visibility.Collapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string str && string.IsNullOrEmpty(str) ? OnNull : OnNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
