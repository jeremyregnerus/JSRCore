// <copyright file="ChangableObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

namespace JSRBaseClassLibrary
{
    /// <summary>
    /// Provides the default implementation of <see cref="IChangableObject"/> interface layered on top of the <see cref="NotifyableObject"/> base class.
    /// </summary>
    public abstract class ChangableObject : NotifyableObject, IChangableObject
    {
        private bool isChanged;

        /// <inheritdoc/>
        public event OnChangedEventHandler OnChanged;

        /// <summary>
        /// Gets a value indicating whether this item has changed.
        /// To include children, override this method and check this.isChanged with Or Else statements on each child.
        /// </summary>
        public bool IsChanged
        {
            get => isChanged;

            private set
            {
                if (value != IsChanged)
                {
                    isChanged = value;
                    NotifyPropertyChanged(nameof(IsChanged));
                    OnChanged?.Invoke(this, isChanged);
                }
            }
        }

        /// <summary>
        /// Accepts the current changes on this object.
        /// To accept changes on ChildOjects, they should override this method and update the children before calling base.AcceptChanges.
        /// </summary>
        public virtual void AcceptChanges()
        {
            IsChanged = false;
        }

        /// <summary>
        /// Sets the value of a property.
        /// Determines if the property's value has changed.
        /// Raises a Change Notification if the property has changed.
        /// </summary>
        /// <typeparam name="T">Type of value of the Property being changed.</typeparam>
        /// <param name="value">The new value for the property.</param>
        /// <param name="backingField">The backing field that contains the exposed property's value.</param>
        /// <param name="propertyName">The name of the property being changed.</param>
        /// <returns>Returns true of the value of the property changed. This value will be false if the value is the same as the previous value.</returns>
        protected override bool SetValue<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
        {
            if ((value != null && !value.Equals(backingField)) || (backingField != null && !backingField.Equals(value)))
            {
                if (typeof(IChangableObject).IsAssignableFrom(typeof(T)) && backingField != null)
                {
                    ((IChangableObject)backingField).OnChanged -= (o, c) => IsChanged = true;
                }

                backingField = value;

                if (typeof(IChangableObject).IsAssignableFrom(typeof(T)) && backingField != null)
                {
                    ((IChangableObject)backingField).OnChanged += (o, c) => IsChanged = true;
                }

                NotifyPropertyChanged(propertyName);
                IsChanged = true;

                return true;
            }

            return false;
        }
    }
}
