// <copyright file="StringExistsToVisibilityConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that provides a <see cref="Visibility"/> value based on the existence of a <see cref="string"/> value.
    /// </summary>
    public class StringExistsToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Evaluates if a string value is given, and returns true or false based on the parameter.
        /// </summary>
        /// <param name="value"><see cref="string"/> value that either does or does not contain text.</param>
        /// <param name="targetType"><inheritdoc/></param>
        /// <param name="parameter">A <see cref="bool"/> specifying if the return value should be inverted.</param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns>A <see cref="bool"/> value based on the existance of text within <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool stringExists = !string.IsNullOrEmpty((string)value);

            if ((parameter is bool b && b) || (parameter is int i && i > 0) || (parameter is string s && s.Equals("true", StringComparison.CurrentCultureIgnoreCase)))
            {
                stringExists = !stringExists;
            }

            return stringExists ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(StringExistsToVisibilityConverter)} only supports One-Way data-binding.");
        }
    }
}
