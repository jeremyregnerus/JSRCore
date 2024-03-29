﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyPropertyChanged"/> with <see cref="IMessenger"/>.
    /// </summary>
    public abstract class ObservableMessenger : Observable, IMessenger
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
                if (SetProperty(ref message, value))
                {
                    OnMessage?.Invoke(this, message);
                }
            }
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
        /// Sets the value for a property.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the value for the property.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed. This will return false if the values are the same.</returns>
        protected override bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(value, field))
            {
                return false;
            }

            RemoveChildMessaging(field);
            field = value;
            AddChildMessaging(field);
            NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Changes the Message of this object when a child raises <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnMessageEventHandler"/>.</param>
        /// <param name="message">Message the child raised.</param>
        protected void OnChildMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
