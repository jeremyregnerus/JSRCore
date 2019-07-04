// <copyright file="MessagingObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IMessenger"/> interface layered on the <see cref="NotifyableObject"/> base class.
    /// </summary>
    [DataContract]
    public abstract class MessagingObject : NotifyableObject, IMessenger
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
            T oldVal = backingField;
            bool retVal = base.SetValue(value, ref backingField, propertyName);

            if (retVal && typeof(IMessenger).IsAssignableFrom(typeof(T)))
            {
                if (oldVal != null)
                {
                    ((IMessenger)oldVal).OnMessage -= ChildRaisedMessage;
                }

                if (backingField != null)
                {
                    ((IMessenger)backingField).OnMessage += ChildRaisedMessage;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Changes the value of Message when a child object raises it's message.
        /// </summary>
        /// <param name="sender"><see cref="IMessenger"/> object.</param>
        /// <param name="message">Message being raised by the child object.</param>
        private void ChildRaisedMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
