// <copyright file="MessagingObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JSRBaseClassLibrary
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IMessenger"/> interface layered on the <see cref="NotifyableObject"/> base class.
    /// </summary>
    public abstract class MessagingObject : NotifyableObject, IMessenger
    {
        private string message;

        /// <summary>
        /// This event should be raised whenever the message for this object is changed.
        /// </summary>
        public event OnMessageEventHandler OnMessage;

        /// <summary>
        /// Gets or sets the Message to raise.
        /// </summary>
        public string Message
        {
            get => message;

            protected set
            {
                if (SetValue(value, ref message))
                {
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <summary>
        /// Sets a new Value for a property.
        /// Checks for equality to determine if the value has changed.
        /// Raises PropertyChanged if the value has changed.
        /// If the property implements IMessenger, removes and adds event notification for Message bubbling.
        /// </summary>
        /// <typeparam name="T">Type of property value.</typeparam>
        /// <param name="value">New value to apply to the property.</param>
        /// <param name="backingField">Backing field of the property.</param>
        /// <param name="propertyName">Name of the property to raise the PropertyChange event for. If ommitted, will use the calling member's name.</param>
        /// <returns>True if the value was changed, false otherwise.</returns>
        protected override bool SetValue<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
        {
            if ((value != null && !value.Equals(backingField)) || (backingField != null && !backingField.Equals(value)))
            {
                if (typeof(IMessenger).IsAssignableFrom(typeof(T)) && backingField != null)
                {
                    ((IMessenger)backingField).OnMessage -= (o, m) => Message = m;
                }

                backingField = value;

                if (typeof(IMessenger).IsAssignableFrom(typeof(T)) && backingField != null)
                {
                    ((IMessenger)backingField).OnMessage += (o, m) => Message = m;
                }

                NotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}
