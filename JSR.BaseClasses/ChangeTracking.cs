// <copyright file="ChangeTracking.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="IChangeTracking"/>.
    /// </summary>
    public abstract class ChangeTracking : IChangeTracking
    {
        private bool isChanged;

        /// <inheritdoc/>
        public virtual bool IsChanged
        {
            get
            {
                if (isChanged)
                {
                    return true;
                }

                foreach (PropertyInfo property in GetType().GetProperties())
                {
                    if (property.GetValue(this) is IChangeTracking tracking)
                    {
                        if (tracking.IsChanged)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            private set => isChanged = value;
        }

        /// <inheritdoc/>
        public virtual void AcceptChanges()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetValue(this) is IChangeTracking tracking)
                {
                    tracking.AcceptChanges();
                }
            }

            IsChanged = false;
        }

        /// <summary>
        /// Sets the value for a property and changes <see cref="IsChanged"/> to true if the value changes.
        /// </summary>
        /// <typeparam name="T">Type of value to set.</typeparam>
        /// <param name="field">Field storing the value for the property.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <returns>True if the value was changed. This will return false if the values are the same.</returns>
        protected virtual bool SetValue<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, field))
            {
                return false;
            }

            field = value;
            IsChanged = true;

            return true;
        }
    }
}
