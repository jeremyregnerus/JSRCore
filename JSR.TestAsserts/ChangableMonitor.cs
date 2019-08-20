// <copyright file="ChangableMonitor.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JSR.BaseClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Monitors the <see cref="PropertyChangedEventHandler"/> and <see cref="OnChangedEventHandler"/> on an <see cref="IChangable"/> object.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IChangable"/>.</typeparam>
    public class ChangableMonitor<T> where T : IChangable
    {
        private readonly T obj;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableMonitor{T}"/> class.
        /// </summary>
        /// <param name="obj">Object to monitor.</param>
        public ChangableMonitor(T obj)
        {
            this.obj = obj;

            PropertiesChanged = new List<string>();
            this.obj.PropertyChanged += (sender, args) => PropertiesChanged.Add(args.PropertyName);

            StateChanges = new List<bool>();
            this.obj.OnChanged += (sender, wasChanged) => StateChanges.Add(wasChanged);
        }

        /// <summary>
        /// Gets a list of properties change notifications since the last ClearNotifications.
        /// </summary>
        public List<string> PropertiesChanged { get; }

        /// <summary>
        /// Gets a list of all of the state change notifications since the last ClearNotifications.
        /// </summary>
        public List<bool> StateChanges { get; }

        /// <summary>
        /// Asserts that the <see cref="PropertyChangedEventHandler"/> and <see cref="OnChangedEventHandler"/> have only raised once since their last reset.
        /// </summary>
        /// <param name="reset">Reset the monitor after asserting notifications.</param>
        public void AssertNotifications(bool reset)
        {
            Assert.AreEqual(1, PropertiesChanged.Count(propertyName => propertyName == nameof(obj.IsChanged)));
            Assert.AreEqual(1, StateChanges.Count);

            if (reset)
            {
                Reset();
            }
        }

        /// <summary>
        /// Clears the current list of changed property notifications.
        /// </summary>
        public void ClearNotifications()
        {
            PropertiesChanged.Clear();
            StateChanges.Clear();
        }

        /// <summary>
        /// Resets the monitored object by accepting changes and clearing the notification lists.
        /// </summary>
        public void Reset()
        {
            obj.AcceptChanges();
            ClearNotifications();
        }
    }
}
