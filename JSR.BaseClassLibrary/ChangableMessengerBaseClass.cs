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

            private set
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
                if (oldValue != null)
                {
                    ((IMessenger)oldValue).OnMessage -= ChildRaisedMessage;
                }

                if (backingField != null)
                {
                    ((IMessenger)backingField).OnMessage += ChildRaisedMessage;
                }
            }

            return retVal;
        }

        private void ChildRaisedMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
