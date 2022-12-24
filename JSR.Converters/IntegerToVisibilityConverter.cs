// <copyright file="IntegerToVisibilityConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that returns a <see cref="Visibility"/> state if the parameter matches the value.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:Element documentation should not be copied and pasted", Justification = "Inherited documentation.")]
    public class IntegerToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns a <see cref="Visibility"/> enumeration of <see cref="Visibility.Visible"/> if the enumeration matches the parameter.
        /// </summary>
        /// <param name="value">An integer value to evaluate against parameter.</param>
        /// <param name="targetType"><inheritdoc/></param>
        /// <param name="parameter">The integer or string containing an integer to check matching against.</param>
        /// <param name="culture"><inheritdoc/></param>
        /// <returns><see cref="Visibility.Visible"/> if <paramref name="value"/> matches <paramref name="parameter"/>, otherwise <see cref="Visibility.Collapsed"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
            {
                return Visibility.Visible;
            }

            string paramStr = string.Empty;

            if (parameter is int i)
            {
                paramStr = i.ToString();
            }
            else if (parameter is string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return Visibility.Visible;
                }

                paramStr = s;
            }

            string[] values = paramStr.Split(new char[] { ',', ';', '|', '/', '\\' });

            if (values.Contains(value.ToString()))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(IntegerToVisibilityConverter)} only supports One-Way data-binding.");
        }
    }
}
