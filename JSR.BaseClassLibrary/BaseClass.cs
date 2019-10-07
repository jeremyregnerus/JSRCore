// <copyright file="BaseClass.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Core base object includes, <see cref="INotifyPropertyChanged"/>, <see cref="IChangeTracking"/> and <see cref="IMessenger"/>.
    /// </summary>
    public abstract class BaseClass : INotifyPropertyChanged, IChangeTracking, IMessenger, INotifyOnChanged
    {
        private bool isChanged;
        private string message;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc/>
        public event OnMessageEventHandler OnMessage;

        /// <inheritdoc/>
        public event OnChangedEventHandler OnChanged;

        /// <inheritdoc/>
        public bool IsChanged
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
            IsChanged = true;
        }

        /// <summary>
        /// Sets the value for a property.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="value">Value to assign to the property.</param>
        /// <param name="backingField">Backingfield that stores the property's value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed. This will return false if the values are the same.</returns>
        protected bool SetValue<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
        {
            if ((value != null && !value.Equals(backingField)) || (backingField != null && !backingField.Equals(value)))
            {
                if (typeof(T) is INotifyOnChanged)
                {
                    RemoveNotifyOnChanged((INotifyOnChanged)backingField);
                    AddNotifyOnChanged((INotifyOnChanged)value);
                }

                if (typeof(T) is IMessenger)
                {
                    RemoveMessenger((IMessenger)backingField);
                    AddMessenger((IMessenger)value);
                }

                backingField = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds tracking for <see cref="OnChanged"/>.
        /// </summary>
        /// <param name="changable">An object that implements <see cref="INotifyOnChanged"/>.</param>
        protected void AddNotifyOnChanged(INotifyOnChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged += OnChildChanged;
            }
        }

        /// <summary>
        /// Removes tracking for <see cref="OnChanged"/>.
        /// </summary>
        /// <param name="changable">An object that implements <see cref="INotifyOnChanged"/>.</param>
        protected void RemoveNotifyOnChanged(INotifyOnChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged -= OnChildChanged;
            }
        }

        /// <summary>
        /// Adds tracking for <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="messenger">An object that implements <see cref="IMessenger"/>.</param>
        protected void AddMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage += OnChildMessage;
            }
        }

        /// <summary>
        /// Removes tracking for <see cref="OnMessage"/>.
        /// </summary>
        /// <param name="messenger">An object that implements <see cref="IMessenger"/>.</param>
        protected void RemoveMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage -= OnChildMessage;
            }
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
