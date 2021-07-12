// <copyright file="MessengerMonitor.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JSR.BaseClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Monitors the <see cref="PropertyChangedEventHandler"/> and <see cref="OnMessageEventHandler"/> on an <see cref="IMessenger"/> object.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IMessenger"/>.</typeparam>
    public class MessengerMonitor<T> where T : IMessenger
    {
        private readonly T obj;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerMonitor{T}"/> class.
        /// </summary>
        /// <param name="obj">Object to monitor.</param>
        public MessengerMonitor(T obj)
        {
            this.obj = obj;

            this.obj.PropertyChanged += (sender, args) => PropertiesChanged.Add(args.PropertyName);
            this.obj.OnMessage += (sender, message) => Messages.Add(message);
        }

        /// <summary>
        /// Gets a list of all the property change notifications since the last <see cref="ClearNotifications"/>.
        /// </summary>
        public List<string> PropertiesChanged { get; } = new List<string>();

        /// <summary>
        /// Gets a list of all the messages raised since the last <see cref="ClearNotifications"/>.
        /// </summary>
        public List<string> Messages { get; } = new List<string>();

        /// <summary>
        /// Asserts that the <see cref="PropertyChangedEventHandler"/> and <see cref="OnMessageEventHandler"/> have only raised a message once since the last <see cref="ClearNotifications"/>.
        /// </summary>
        /// <param name="message">Message to test.</param>
        /// <param name="clearNotifications">True to clear existing notification, false otherwise.</param>
        public void AssertMessageNotification(string message, bool clearNotifications)
        {
            Assert.AreEqual(message, obj.Message);
            CollectionAssert.Contains(Messages, message);

            if (clearNotifications)
            {
                ClearNotifications();
            }
        }

        /// <summary>
        /// Asserts that the <see cref="PropertyChangedEventHandler"/> and <see cref="OnMessageEventHandler"/> were raised a specific number of times since the <see cref="ClearNotifications"/>.
        /// </summary>
        /// <param name="count">Number of expected messages raised.</param>
        /// <param name="clearNotifications">True to clear existing notifications, false otherwise.</param>
        public void AssertMessageCount(int count, bool clearNotifications)
        {
            Assert.AreEqual(count, PropertiesChanged.Count(propertyName => propertyName == nameof(obj.Message)));
            Assert.AreEqual(count, Messages.Count);

            if (clearNotifications)
            {
                ClearNotifications();
            }
        }

        /// <summary>
        /// Clears the current list of changed properties and messages.
        /// </summary>
        public void ClearNotifications()
        {
            PropertiesChanged.Clear();
            Messages.Clear();
        }
    }
}
