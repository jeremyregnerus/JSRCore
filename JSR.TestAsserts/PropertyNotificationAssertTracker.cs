// <copyright file="PropertyNotificationAssertTracker.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tracks property change notifications and asserts.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
    public class PropertyNotificationAssertTracker<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNotificationAssertTracker{T}"/> class.
        /// </summary>
        public PropertyNotificationAssertTracker() : this(Activator.CreateInstance<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNotificationAssertTracker{T}"/> class.
        /// </summary>
        /// <param name="obj">Object to track.</param>
        public PropertyNotificationAssertTracker(T obj)
        {
            TrackedObject = obj;
            TrackedObject.PropertyChanged += (sender, args) => PropertiesChanged.Add(args.PropertyName);
        }

        /// <summary>
        /// Gets object which is being tracked and evaluated.
        /// </summary>
        public T TrackedObject { get; }

        /// <summary>
        /// Gets list of the properties that have raised notification for the tracked object.
        /// </summary>
        public List<string> PropertiesChanged { get; } = new List<string>();

        /// <summary>
        /// Gets number of properties that have changed since the last reset.
        /// </summary>
        public int PropertiesChangedCount => PropertiesChanged.Count;

        /// <summary>
        /// Evaluates that the names property has been raised any number of times.
        /// </summary>
        /// <param name="propertyName">Name of property to test.</param>
        public void AssertPropertyRaised(string propertyName)
        {
            CollectionAssert.Contains(PropertiesChanged, propertyName);
        }

        /// <summary>
        /// Evaluates that the names property has appeared the number of times specified.
        /// </summary>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="expectedCount">Expected times the property has raised notification.</param>
        public void AssertPropertyCount(string propertyName, int expectedCount)
        {
            Assert.AreEqual(expectedCount, PropertiesChanged.Count(nameInList => nameInList == propertyName));
        }

        /// <summary>
        /// Clears all of the current notifications for the tracked object.
        /// </summary>
        public virtual void ClearNotifications()
        {
            PropertiesChanged.Clear();
        }

        /// <summary>
        /// Resets all of the testing parameters for the tracked object.
        /// </summary>
        public virtual void Reset()
        {
            ClearNotifications();
        }
    }
}
