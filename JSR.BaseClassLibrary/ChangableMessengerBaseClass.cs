// <copyright file="ChangableMessengerBaseClass.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Provides an implementation of the <see cref="IMessenger"/> interface layered on the <see cref="ChangableBaseClass"/> base class.
    /// </summary>
    [DataContract]
    public abstract class ChangableMessengerBaseClass : ChangableBaseClass, IChangableMessenger
    {
        private string message;

        /// <inheritdoc/>
        public event OnMessageEventHandler OnMessage;

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

        /// <inheritdoc/>
        protected override bool SetValue<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
        {
            T oldValue = backingField;
            bool retVal = base.SetValue(value, ref backingField, propertyName);

            if (retVal && typeof(IMessenger).IsAssignableFrom(typeof(T)))
            {
                RemoveMessenger((IMessenger)oldValue);
                AddMessenger((IMessenger)backingField);
            }

            return retVal;
        }

        /// <summary>
        /// Monitors the <see cref="OnMessage"/> event to a child object.
        /// </summary>
        /// <param name="messenger">Child object to add.</param>
        protected void AddMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage += ChildRaisedMessage;
            }
        }

        /// <summary>
        /// Removes the <see cref="OnMessage"/> event from a child object.
        /// </summary>
        /// <param name="messenger">Child object to remove.</param>
        protected void RemoveMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage -= ChildRaisedMessage;
            }
        }

        /// <summary>
        /// Adds notification tracking to an <see cref="IChangableMessenger"/> object.
        /// </summary>
        /// <param name="changableMessenger">Object to track notification.</param>
        /// <typeparam name="T">Type that implements <see cref="IMessenger"/> and <see cref="INotifyOnChanged"/>.</typeparam>
        protected void AddChangableMessenger<T>(T changableMessenger) where T : IMessenger, INotifyOnChanged
        {
            AddChangeTracking(changableMessenger);
            AddMessenger(changableMessenger);
        }

        /// <summary>
        /// Removes notification tracking from an <see cref="IChangableMessenger"/> object.
        /// </summary>
        /// <param name="changableMessenger">Object to remove notification tracking from.</param>
        /// <typeparam name="T">Type that implements <see cref="IMessenger"/> and <see cref="INotifyOnChanged"/>.</typeparam>
        protected void RemoveChangableMessenger<T>(T changableMessenger) where T : IMessenger, INotifyOnChanged
        {
            RemoveChangeTracking(changableMessenger);
            RemoveMessenger(changableMessenger);
        }

        private void ChildRaisedMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
