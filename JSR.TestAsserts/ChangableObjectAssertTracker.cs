// <copyright file="ChangableObjectAssertTracker.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JSR.BaseClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Manages the <see cref="INotifyPropertyChanged"/> and <see cref="IChangeTracking"/> events found in the <see cref="IChangableObject"/> interface.
    /// </summary>
    /// <typeparam name="T">Type the object is tracking notifications for.</typeparam>
    public class ChangableObjectAssertTracker<T> : PropertyNotificationAssertTracker<T> where T : IChangableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableObjectAssertTracker{T}"/> class.
        /// </summary>
        public ChangableObjectAssertTracker() : this(Activator.CreateInstance<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableObjectAssertTracker{T}"/> class.
        /// </summary>
        /// <param name="obj">Object to track.</param>
        public ChangableObjectAssertTracker(T obj) : base(obj)
        {
            TrackedObject.OnChanged += (sender, wasChanged) => StateChanges.Add(wasChanged);

            Reset();
        }

        /// <summary>
        /// Gets list of each time the object's IsChanged state has changed.
        /// </summary>
        public List<bool> StateChanges { get; } = new List<bool>();

        /// <summary>
        /// Gets number of times the <see cref="IChangableObject.OnChanged"/> was raised with a true value.
        /// </summary>
        public int WasChangedCount => StateChanges.Count(wasChanged => wasChanged == true);

        /// <summary>
        /// Gets number of times the <see cref="INotifyPropertyChanged.PropertyChanged"/> was raised with the IsChanged property.
        /// </summary>
        public int IsChangedPropertyCount => PropertiesChanged.Count(propertyName => propertyName == nameof(IChangableObject.IsChanged));

        /// <summary>
        /// Tests that regardless of the number of changes made, the test object only raised <see cref="IChangableObject.OnChanged"/> and changed the IsChanged property once.
        /// </summary>
        public void AssertWasChanged()
        {
            Assert.AreEqual(1, IsChangedPropertyCount);
            Assert.AreEqual(1, WasChangedCount);
        }

        /// <summary>
        /// Clears the current lists of notifications.
        /// </summary>
        public override void ClearNotifications()
        {
            base.ClearNotifications();

            StateChanges.Clear();
        }

        /// <summary>
        /// Resets the changes on the object and clears the current lists of notifications.
        /// </summary>
        public override void Reset()
        {
            TrackedObject.AcceptChanges();
            ClearNotifications();
        }
    }
}
