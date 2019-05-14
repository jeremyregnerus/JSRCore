// <copyright file="NotifyableObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JSRBaseClassLibrary
{
    /// <summary>
    /// Provides a default implementation of the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public abstract class NotifyableObject
    {
        /// <summary>
        /// Event Handler referenced when property values are changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises PropertyChanged for every property on the Object.
        /// </summary>
        protected void NotifyAllPropertiesChanged()
        {
            foreach (var p in GetType().GetRuntimeProperties())
            {
                NotifyPropertyChanged(p.Name);
            }
        }

        /// <summary>
        /// Raise the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property to raise the PropertyChange event for.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a new Value for a property.
        /// Checks for equality to determine if the value has changed.
        /// Raises PropertyChanged if the value has changed..
        /// </summary>
        /// <typeparam name="T">Type of property value.</typeparam>
        /// <param name="value">New value to apply to the property.</param>
        /// <param name="backingField">Backing field of the property.</param>
        /// <param name="propertyName">Name of the property to raise the PropertyChange event for. If ommitted, will use the calling member's name.</param>
        /// <returns>True if the value was changed, false otherwise.</returns>
        protected virtual bool SetValue<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
        {
            if ((value != null && !value.Equals(backingField)) || (backingField != null && !backingField.Equals(value)))
            {
                backingField = value;
                NotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}
