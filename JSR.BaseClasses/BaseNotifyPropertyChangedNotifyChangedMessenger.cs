// <copyright file="BaseNotifyPropertyChangedNotifyChangedMessenger.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Implements <see cref="INotifyPropertyChanged"/>, <see cref="IChangeTracking"/>, <see cref="INotifyChanged"/> and <see cref="IMessenger"/>.
    /// </summary>
    public abstract class BaseNotifyPropertyChangedNotifyChangedMessenger : INotifyPropertyChanged, IChangeTracking, INotifyChanged, IMessenger
    {
        private bool isChanged = true;
        private string message = string.Empty;

        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public event OnMessageEventHandler? OnMessage;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public virtual bool IsChanged
        {
            get
            {
                if (isChanged)
                {
                    return true;
                }

                foreach (PropertyInfo property in GetType().GetProperties())
                {
                    if (property.GetValue(this) is IChangeTracking tracking && tracking.IsChanged)
                    {
                        return true;
                    }
                }

                return false;
            }

            protected set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    NotifyPropertyChanged();
                    OnChanged?.Invoke(this, isChanged);
                }
            }
        }

        /// <inheritdoc/>
        public string Message
        {
            get => message;
            protected set
            {
                if (value != message)
                {
                    message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <inheritdoc/>
        public virtual void AcceptChanges()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetValue(this) is IChangeTracking tracking)
                {
                    tracking.AcceptChanges();
                }
            }

            IsChanged = false;
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for all property objects that implement those interfaces.
        /// </summary>
        protected void AddChildNotifications()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                AddChildNotifications(property.GetValue(this));
            }
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for an object that implements those interfaces.
        /// </summary>
        /// <typeparam name="T">Type of object to watch for notifications.</typeparam>
        /// <param name="child">Child object to watch for notifications.</param>
        protected void AddChildNotifications<T>(T child)
        {
            if (child is INotifyChanged notifyChanged)
            {
                notifyChanged.OnChanged += OnChildChanged;
            }

            if (child is IMessenger messenger)
            {
                messenger.OnMessage += OnChildMessage;
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChangedEventHandler"/> for ever property within the object.
        /// </summary>
        protected void NotifyAllPropertiesChanged()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                NotifyPropertyChanged(property.Name);
            }
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

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for all property objects that implenment those interfaces.
        /// </summary>
        protected void RemoveChildNotifications()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                RemoveChildNotifications(property.GetValue(this));
            }
        }

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> and <see cref="IMessenger"/> notification tracking for an object that implenments those interfaces.
        /// </summary>
        /// <typeparam name="T">Type of object to no longer watch for notifications.</typeparam>
        /// <param name="child">Child object to no longer watch for notifications.</param>
        protected void RemoveChildNotifications<T>(T child)
        {
            if (child is INotifyChanged notifyChanged)
            {
                notifyChanged.OnChanged -= OnChildChanged;
            }

            if (child is IMessenger messenger)
            {
                messenger.OnMessage -= OnChildMessage;
            }
        }

        /// <summary>
        /// Sets the value for a property.
        /// If the value changes, this method raises the <see cref="PropertyChanged"/> event.
        /// If the property value implements <see cref="IChangeTracking"/> this method manages tracking those events.
        /// If the property value implements <see cref="IMessenger"/> this method manages tracking those events.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the property value.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed. false if the value was the same and not changed.</returns>
        protected virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(value, field))
            {
                return false;
            }

            RemoveChildNotifications(field);
            field = value;
            AddChildNotifications(value);

            NotifyPropertyChanged(propertyName);

            IsChanged = true;
            return true;
        }

        /// <summary>
        /// Changes the <see cref="IsChanged"/> value of this object when a child raises <see cref="OnChanged"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnChangedEventHandler"/>.</param>
        /// <param name="wasChanged">True if the object was changed, false otherwise.</param>
        private void OnChildChanged(object sender, bool wasChanged)
        {
            IsChanged = true;
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
