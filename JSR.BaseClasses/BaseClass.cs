// <copyright file="BaseClass.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Core base object includes, <see cref="INotifyPropertyChanged"/>, <see cref="IChangeTracking"/> and <see cref="IMessenger"/>.
    /// </summary>
    [DataContract]
    public abstract class BaseClass : INotifyChanged, INotifyPropertyChanged, IChangeTracking, IMessenger
    {
        private bool isChanged = true;
        private string message = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClass"/> class.
        /// </summary>
        public BaseClass()
        {
            OnCreated();
        }

        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public event OnMessageEventHandler? OnMessage;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public virtual bool IsChanged
        {
            get => isChanged;
            protected set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChanged)));
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
                var val = property.GetValue(this);

                if (val is IChangeTracking ct)
                {
                    ct.AcceptChanges();
                }
            }

            IsChanged = false;
        }

        /// <summary>
        /// Adds notification tracking for <see cref="INotifyChanged"/> and <see cref="IMessenger"/> child object.
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
        /// Adds notification tracking for <see cref="INotifyChanged"/> child objects.
        /// </summary>
        /// <typeparam name="T">Type of objects to add notification tracking.</typeparam>
        /// <param name="children">Objects to add notification tracking.</param>
        protected void AddChildNotifications<T>(IEnumerable<T> children)
        {
            foreach (T child in children)
            {
                AddChildNotifications(child);
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
        /// This method should run after an object is created to add notification tracking to child objects.
        /// </summary>
        protected virtual void OnCreated()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                AddChildNotifications(property.GetValue(this));
            }
        }

        /// <summary>
        /// Called when this object is deserialized.
        /// </summary>
        /// <param name="s">Streaming context.</param>
        [OnDeserialized]
        protected virtual void OnDeserialized(StreamingContext s)
        {
            OnCreated();
            IsChanged = false;
        }

        /// <summary>
        /// Removes notification tracking for <see cref="INotifyChanged"/> and <see cref="IMessenger"/> child objects.
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
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the value for the property.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed. This will return false if the values are the same.</returns>
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
        /// Changes the <see cref="IsChanged"/> state of this object when a child raises <see cref="OnChanged"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnChangedEventHandler"/>.</param>
        /// <param name="wasChanged">True if the object was changed, false otherwise.</param>
        private void OnChildChanged(object sender, bool wasChanged)
        {
            IsChanged = true;
        }

        /// <summary>
        /// Changes the Message of this object when a child raises <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnMessageEventHandler"/>.</param>
        /// <param name="message">Message the child raised.</param>
        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
