using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Implements <see cref="INotifyPropertyChanged"/>, <see cref="IChangeTracking"/>, <see cref="INotifyChanged"/> and <see cref="IMessenger"/>.
    /// </summary>
    public abstract class ObservableChangeableMessenger : ObservableChangeable, IMessenger
    {
        private string message = string.Empty;

        /// <inheritdoc/>
        public event OnMessageEventHandler? OnMessage;

        /// <inheritdoc/>
        public string Message
        {
            get => message;
            protected set
            {
                if (value != message)
                {
                    message = value;
                    NotifyPropertyChanged();
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <summary>
        /// Sets the value for a property.
        /// If the value changes, this method raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
        /// If the property value implements <see cref="IChangeTracking"/> this method manages tracking those events.
        /// If the property value implements <see cref="IMessenger"/> this method manages tracking those events.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the property value.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed. otherwise false.</returns>
        protected override bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(value, field))
            {
                return false;
            }

            RemoveChildChangeTracking(field);
            RemoveChildMessaging(field);

            field = value;

            AddChildChangeTracking(value);
            AddChildMessaging(value);

            NotifyPropertyChanged(propertyName);

            IsChanged = true;
            return true;
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for an object that implements those interfaces.
        /// </summary>
        /// <typeparam name="T">Type of object to watch for notifications.</typeparam>
        /// <param name="child">Child object to watch for notifications.</param>
        protected void AddChildMessaging<T>(T child)
        {
            if (child is IMessenger messenger)
            {
                messenger.OnMessage += OnChildMessage;
            }
        }

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for an object that implenments those interfaces.
        /// </summary>
        /// <typeparam name="T">Type of object to no longer watch for notifications.</typeparam>
        /// <param name="child">Child object to no longer watch for notifications.</param>
        protected void RemoveChildMessaging<T>(T child)
        {
            if (child is IMessenger messenger)
            {
                messenger.OnMessage -= OnChildMessage;
            }
        }

        /// <summary>
        /// Changes the <see cref="Message"/> of this object when a child raises <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnMessageEventHandler"/>.</param>
        /// <param name="message">Message the child raised.</param>
        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
