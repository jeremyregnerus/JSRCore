namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="IMessenger"/>.
    /// </summary>
    public abstract class Messenger : IMessenger
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
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <summary>
        /// Sets the value for a property, and manages <see cref="IMessenger"/> tracking if the object supports it.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the value for the property.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <returns>True if the value was changed. This will return false if the values are the same.</returns>
        protected virtual bool SetProperty<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            RemoveChildMessaging(field);
            field = value;
            AddChildMessaging(field);

            return true;
        }

        /// <summary>
        /// Adds <see cref="IMessenger"/> notification tracking for an object if it implements <see cref="IMessenger"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to track notifications.</typeparam>
        /// <param name="child">Object to track notifications.</param>
        protected void AddChildMessaging<T>(T child)
        {
            if (child is IMessenger messenger)
            {
                messenger.OnMessage += OnChildMessage;
            }
        }

        /// <summary>
        /// Removes <see cref="IMessenger"/> notification tracking from an object if it implements <see cref="IMessenger"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to no longer track notifications.</typeparam>
        /// <param name="child">Object to no longer track notifications.</param>
        protected void RemoveChildMessaging<T>(T child)
        {
            if (child is IMessenger messenger)
            {
                messenger.OnMessage -= OnChildMessage;
            }
        }

        /// <summary>
        /// Changes the Message of this object when a child object raises <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnMessageEventHandler"/>.</param>
        /// <param name="message">Message the child raised.</param>
        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
