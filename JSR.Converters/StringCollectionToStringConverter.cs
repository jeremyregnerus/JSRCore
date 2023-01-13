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
            List<string> sortableList = new();

            if (value is IEnumerable<string> list)
            {
                sortableList.AddRange(list);
            }

            if (value is StringCollection sc)
            {
                foreach (string s in sc.OfType<string>())
                {
                    sortableList.Add(s);
                }
            }

            sortableList.Sort();

            string output = string.Empty;

            for (int i = 0; i < sortableList.Count; i++)
            {
                output += sortableList[i];

                if (i < sortableList.Count - 1)
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
