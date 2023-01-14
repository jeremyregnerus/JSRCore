using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Sets the value for a property.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the  property value.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed, otherwise false.</returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raise the <see cref="PropertyChangedEventHandler"/> for multiple properties.
        /// </summary>
        /// <param name="propertyNames">Array of property names to raise the event handler.</param>
        protected void NotifyPropertiesChanged(string[] propertyNames)
        {
            if (propertyNames != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    NotifyPropertyChanged(propertyName);
                }
            }
        }

        /// <summary>
        /// Raise the <see cref="PropertyChangedEventHandler"/> for multiple properties.
        /// </summary>
        /// <param name="propertyNames">List of property names to raise the event handlder.</param>
        protected void NotifyPropertiesChanged(List<string> propertyNames)
        {
            if (propertyNames != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    NotifyPropertyChanged(propertyName);
                }
            }
        }

        /// <summary>
        /// Raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="propertyName">Property name to raise the event handler.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
