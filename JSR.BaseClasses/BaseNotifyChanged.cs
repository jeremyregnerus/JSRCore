// <copyright file="BaseNotifyChanged.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyChanged"/>.
    /// </summary>
    public abstract class BaseNotifyChanged : BaseChangeTracking, INotifyChanged
    {
        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public override bool IsChanged
        {
            get
            {
                if (base.IsChanged)
                {
                    return true;
                }

                foreach (PropertyInfo property in GetType().GetProperties())
                {
                    if (property.GetValue(this) is IChangeTracking tracking && tracking.IsChanged)
                    {
                        return true;
                    }
                }

                return false;
            }

            protected set
            {
                if (value != base.IsChanged)
                {
                    base.IsChanged = value;
                    OnChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> notification tracking for all property objects that implement <see cref="INotifyChanged"/>.
        /// </summary>
        protected void AddChildChangeTracking()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                AddChildChangeTracking(property.GetValue(this));
            }
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> notification tracking for an object if it implements <see cref="INotifyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to track notifications.</typeparam>
        /// <param name="child">Object to track notifications.</param>
        protected void AddChildChangeTracking<T>(T child)
        {
            if (child is INotifyChanged changed)
            {
                changed.OnChanged += OnChildChanged;
            }
        }

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> notification tracking for all property objects that implement <see cref="INotifyChanged"/>.
        /// </summary>
        protected void RemoveChildChangeTracking()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                RemoveChildChangeTracking(property.GetValue(this));
            }
        }

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> notification tracking from an object if it implements <see cref="INotifyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to no longer track notifications.</typeparam>
        /// <param name="child">Object to no longer track notifications.</param>
        protected void RemoveChildChangeTracking<T>(T child)
        {
            if (child is INotifyChanged changed)
            {
                changed.OnChanged -= OnChildChanged;
            }
        }

        /// <summary>
        /// Changes <see cref="IsChanged"/> when a child raises <see cref="OnChanged"/> with a value of true.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnChangedEventHandler"/>.</param>
        /// <param name="wasChanged">True if the object was changed, otherwise false.</param>
        private void OnChildChanged(object sender, bool wasChanged)
        {
            if (wasChanged)
            {
                IsChanged = true;
            }
        }
    }
}
