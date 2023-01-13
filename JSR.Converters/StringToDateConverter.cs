using System.Globalization;
using System.Windows.Data;

namespace JSR.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/> for converting <see cref="string"/> to <see cref="DateTime"/>.
    /// </summary>
    public class StringToDateConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse((string)value, out DateTime date))
            {
                return date;
            }
            else
            {
                return DateTime.Now;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime d)
            {
                return d.Date.ToString("d");
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
