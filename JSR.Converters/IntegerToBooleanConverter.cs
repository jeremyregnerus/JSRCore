// <copyright file="IntegerToBooleanConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Globalization;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// Converts an <see cref="int"/> to a <see cref="bool"/> by comparing it to a provided parameter.
    /// </summary>
    public class IntegerToBooleanConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == System.Convert.ToInt32(parameter);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : Binding.DoNothing;
        }
    }
}
