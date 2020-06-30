// <copyright file="BooleanToInverseBooleanConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> for converting the inverse of a <see cref="bool"/> to a <see cref="bool"/> value.
    /// </summary>
    public class BooleanToInverseBooleanConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
