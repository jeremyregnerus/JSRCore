// <copyright file="StringCollectionToStringConverter.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> that converts a <see cref="StringCollection"/> to a <see cref="string"/>.
    /// </summary>
    public class StringCollectionToStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringCollection stringCollection = new StringCollection();

            if (value is StringCollection sc)
            {
                stringCollection = sc;
            }

            List<string> list = new List<string>();

            foreach (string item in stringCollection)
            {
                list.Add(item);
            }

            list.Sort();

            string output = string.Empty;

            for (int i = 0; i < list.Count; i++)
            {
                output += list[i];

                if (i < list.Count - 1)
                {
                    output += Environment.NewLine;
                }
            }

            return output;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(StringCollectionToStringConverter)} only supports One-Way data-binding.");
        }
    }
}
