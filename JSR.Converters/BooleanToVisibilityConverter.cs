// <copyright file="BooleanToVisibilityConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> for converting a <see cref="bool"/> to a <see cref="Visibility"/> value.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:Element documentation should not be copied and pasted", Justification = "Inherited documentation.")]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="bool"/> value to a <see cref="Visibility"/> enumeration.
        /// </summary>
        /// <param name="value">A boolean value indicating whether the converted value should be <see cref="Visibility.Visible"/> or <see cref="Visibility.Collapsed"/>.</param>
        /// <param name="targetType"><inheritdoc/></param>
        /// <param name="parameter">A boolean value that specifies if true, the return value should be inverted.</param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns>A <see cref="Visibility"/> value based on the conversion of <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;

            if ((parameter is bool b && b) || (parameter is int i && i > 0) || (parameter is string s && s.Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                isVisible = !isVisible;
            }

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> enumeration to a <see cref="bool"/>.
        /// </summary>
        /// <param name="value">A <see cref="Visibility"/> enumeration.</param>
        /// <param name="targetType"><inheritdoc/><inheritdoc/></param>
        /// <param name="parameter">A boolean value that specifies if true, the return value should be inverted.</param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns>A <see cref="bool"/> value based on the conversion of <paramref name="value"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (Visibility)value == Visibility.Visible;

            if (parameter is bool invert && invert)
            {
                isVisible = !isVisible;
            }

            return isVisible;
        }
    }
}
