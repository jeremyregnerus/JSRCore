// <copyright file="StringCollectionToStringArrayConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections.Specialized;
using System.Globalization;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that converts a <see cref="StringCollection"/> to an <see cref="Array"/> of <see cref="string"/>.
    /// </summary>
    public class StringCollectionToStringArrayConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StringCollection sc)
            {
                string[] list = new string[sc.Count];
                sc.CopyTo(list, 0);
                return list;
            }

            return Array.Empty<string>();
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringCollection sc = new();

            if (value is string[] list)
            {
                sc.AddRange(list);
            }

            return sc;
        }
    }
}
